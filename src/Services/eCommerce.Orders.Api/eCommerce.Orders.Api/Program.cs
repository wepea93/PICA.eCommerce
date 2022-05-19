using eCommerce.Orders.Api.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System.Reflection;
using System.Text;
using eCommerce.Orders.Core.Config;
using eCommerce.Orders.Core.Contracts.Repositories;
using eCommerce.Orders.Core.Contracts.Services;
using eCommerce.Orders.Core.Helpers.BadRequests;
using eCommerce.Orders.Core.Helpers.Mappers;
using eCommerce.Orders.Core.Objects.Responses;
using eCommerce.Orders.Infraestructure.Models.UnitOfWorks;
using eCommerce.Orders.Infraestructure.Repositories;
using eCommerce.Orders.Infraestructure.Services;
using eCommerce.Orders.Infraestructure.Contexts.DbOrder;
using eCommerce.Orders.Core.Helpers.Log;
using eCommerce.Orders.Core.Publisher;
using eCommerce.Commons.Objects.Messaging;
using eCommerce.PublisherSubscriber.Contracts;
using eCommerce.Commons.HealthChecks;
using Microsoft.AspNetCore.Authorization;
using eCommerce.Commons.Security;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = AppConfiguration.Configuration["AppConfiguration:ApiSwaggerName"].ToString(), Version = "v1" });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    //c.AddSecurityRequirement(new OpenApiSecurityRequirement
    //{
    //    {
    //        new OpenApiSecurityScheme
    //        {
    //            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
    //        },
    //        new string[]{}
    //    }
    //});
    //c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    //{
    //    Description = "Use bearer token to authorize (enter into field the word 'Bearer' following by space and JWT)",
    //    Type = SecuritySchemeType.ApiKey,
    //    Scheme = "bearer",
    //    BearerFormat = "JWT",
    //    Name = "Authorization",
    //    In = ParameterLocation.Header,
    //});
});

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//.AddJwtBearer((options) =>
//{
//    //options.Authority = "https://localhost:5247";
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateLifetime = true,
//        ValidateAudience = false
//    };
//});


builder.Services.AddMvc()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = actionContext =>
        {
            var modelState = actionContext.ModelState;
            return new BadRequestObjectResult(new ServiceResponse<Dictionary<string, string[]>>(
                "Bad request", BadRequestHelper.GetValidationResult(modelState)));
        };
    });

#region RegisterLog

var levelSwitch = new LoggingLevelSwitch();
levelSwitch.MinimumLevel = LogEventLevel.Information;

var basePath = AppConfiguration.Configuration["AppConfiguration:Log:SeqFilePath"].ToString() + "\\" + AppConfiguration.Configuration["AppConfiguration:ApiCode"].ToString();
if (!System.IO.Directory.Exists(basePath))
    System.IO.Directory.CreateDirectory(basePath);

var filePath = "[BASEPATH]\\" + "Log-[DATE].txt";
filePath = filePath.Replace("[BASEPATH]", basePath);
filePath = filePath.Replace("[DATE]", DateTime.Now.ToString("yyyy-MM-dd"));
var x=0;

builder.Host.UseSerilog((ctx, lc) => lc
    .MinimumLevel.ControlledBy(levelSwitch)
    .Enrich.WithProperty("Application", "API-" + AppConfiguration.Configuration["AppConfiguration:ApiCode"].ToString())
    .WriteTo.Seq(AppConfiguration.Configuration["AppConfiguration:Log:SeqHost"].ToString(),
        apiKey: AppConfiguration.Configuration["AppConfiguration:Log:SeqApiKey"].ToString(),
        bufferBaseFilename: filePath,
        controlLevelSwitch: levelSwitch));

#endregion

builder.Services.AddDbContext<DbOrderContext>(
    options => options.UseSqlServer(AppConfiguration.Configuration["AppConfiguration:DataBases:DataBase1:ConnectionString"].ToString()));

builder.Services.AddTransient<IPublisher<OrderMsg>, PublisherOrderMsg>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddSingleton<ILogHelper, LogHelper>();
builder.Services.AddScoped<IMapperHelper, MapperHelper>();
builder.Services.AddScoped<IOrderService, OrderService>();

#region AddHealthChecks

builder.Services.AddHealthChecks()
    .AddSqlServer(
        connectionString: AppConfiguration.Configuration["AppConfiguration:DataBases:DataBase1:ConnectionString"].ToString(),
        healthQuery: "SELECT 1;",
        name: "SqlServerContext")
    .AddCheck<MemoryHealthCheck>("Memory");

#endregion HealthChecks

#region Authentication

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = AppConfiguration.Configuration["AppConfiguration:Authentication:Authority"].ToString();
    options.Audience = AppConfiguration.Configuration["AppConfiguration:Authentication:Audience"].ToString();
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("create:order", policy => policy.Requirements.Add(new HasScopeRequirement("create:order", AppConfiguration.Configuration["AppConfiguration:Authentication:Authority"].ToString())));
    options.AddPolicy("update:order", policy => policy.Requirements.Add(new HasScopeRequirement("update:order", AppConfiguration.Configuration["AppConfiguration:Authentication:Authority"].ToString())));
    options.AddPolicy("read:order", policy => policy.Requirements.Add(new HasScopeRequirement("read:order", AppConfiguration.Configuration["AppConfiguration:Authentication:Authority"].ToString())));
});
builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
        c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", AppConfiguration.Configuration["AppConfiguration:ApiSwaggerName"].ToString());
    });
}

app.Use(async (context, next) =>
{
    context.Request.EnableBuffering();

    using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, false, 1024, true))
    {
        var body = await reader.ReadToEndAsync();
        context.Request.Body.Seek(0, SeekOrigin.Begin);
    }

    await next.Invoke();
});

app.UseHealthChecks("/healthz", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        var response = new HealthCheckResponse
        {
            Status = report.Status.ToString(),
            HealtChecks = report.Entries.Select(x => new IndividualHealthCheckResponse
            {
                Component = x.Key,
                Status = x.Value.Status.ToString(),
                Description = x.Value.Description
            }),
            HealthCheckDuration = report.TotalDuration
        };
        await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
    }
});

ILogHelper logHelper = app.Services.GetRequiredService<ILogHelper>();
app.ConfigureExceptionHandler(logHelper);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
