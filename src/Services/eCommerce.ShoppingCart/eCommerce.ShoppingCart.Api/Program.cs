using eCommerce.ShoppingCart.Api.Config;
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
using eCommerce.ShoppingCart.Core.Config;
using eCommerce.ShoppingCart.Core.Contracts.Repositories;
using eCommerce.ShoppingCart.Core.Contracts.Services;
using eCommerce.ShoppingCart.Core.Helpers.BadRequests;
using eCommerce.ShoppingCart.Core.Helpers.Log;
using eCommerce.ShoppingCart.Core.Helpers.Mappers;
using eCommerce.ShoppingCart.Infraestructure.Contexts.UnitOfWorks;
using eCommerce.ShoppingCart.Infraestructure.Repositories;
using eCommerce.ShoppingCart.Infraestructure.Services;
using eCommerce.Commons.Objects.Responses;
using eCommerce.ShoppingCart.Infraestructure.Contexts;
using eCommerce.Commons.HealthChecks;
using eCommerce.Commons.Objects.Responses.HealthCheck;
using eCommerce.ShoppingCart.Infraestructure.Contexts.DbProduct;
using eCommerce.ShoppingCart.Core.Consumer;
using Microsoft.AspNetCore.Authorization;
using eCommerce.Commons.Security;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.AllowAnyOrigin()
                                    .AllowAnyHeader().
                                        AllowAnyMethod();
                      });
});

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

var levelSwitch = new LoggingLevelSwitch();
levelSwitch.MinimumLevel = LogEventLevel.Information;

var basePath = AppConfiguration.Configuration["AppConfiguration:Log:SeqFilePath"].ToString() + "\\" + AppConfiguration.Configuration["AppConfiguration:ApiCode"].ToString();
if (!System.IO.Directory.Exists(basePath))
    System.IO.Directory.CreateDirectory(basePath);

var filePath = "[BASEPATH]\\" + "Log-[DATE].txt";
filePath = filePath.Replace("[BASEPATH]", basePath);
filePath = filePath.Replace("[DATE]", DateTime.Now.ToString("yyyy-MM-dd"));

builder.Host.UseSerilog((ctx, lc) => lc
    .MinimumLevel.ControlledBy(levelSwitch)
    .Enrich.WithProperty("Application", "API-" + AppConfiguration.Configuration["AppConfiguration:ApiCode"].ToString())
    .WriteTo.Seq(AppConfiguration.Configuration["AppConfiguration:Log:SeqHost"].ToString(),
        apiKey: AppConfiguration.Configuration["AppConfiguration:Log:SeqApiKey"].ToString(),
        bufferBaseFilename: filePath,
        controlLevelSwitch: levelSwitch));


builder.Services.AddDbContext<DbShoppingCartReadContext>(
    options => options.UseSqlServer(AppConfiguration.Configuration["AppConfiguration:DataBases:EcommerceCoreWrite:ConnectionString"].ToString()));

builder.Services.AddDbContext<DbShoppingCartWriteContext>(
    options => options.UseSqlServer(AppConfiguration.Configuration["AppConfiguration:DataBases:EcommerceCoreWrite:ConnectionString"].ToString()));

builder.Services.AddDbContext<DbProductContext>(
    options => options.UseSqlServer(AppConfiguration.Configuration["AppConfiguration:DataBases:EcommerceProducts:ConnectionString"].ToString()));

//builder.Services.AddHostedService<ConsumerOrderMsg>();

builder.Services.AddScoped<IShoppingCartReadRepository, ShoppingCartReadRepository>();
builder.Services.AddScoped<IShoppingCartWriteRepository, ShoppingCartWriteRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddSingleton<ILogHelper, LogHelper>();
builder.Services.AddScoped<IMapperHelper, MapperHelper>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();

builder.Services.AddHealthChecks()
    .AddSqlServer(
        connectionString: AppConfiguration.Configuration["AppConfiguration:DataBases:EcommerceCoreWrite:ConnectionString"].ToString(),
        healthQuery: "SELECT 1;",
        name: "EcommerceCoreWriteContext")
     .AddSqlServer(
        connectionString: AppConfiguration.Configuration["AppConfiguration:DataBases:EcommerceCoreRead:ConnectionString"].ToString(),
        healthQuery: "SELECT 1;",
        name: "EcommerceCoreReadContext")
      .AddSqlServer(
        connectionString: AppConfiguration.Configuration["AppConfiguration:DataBases:EcommerceProducts:ConnectionString"].ToString(),
        healthQuery: "SELECT 1;",
        name: "EcommerceProductContext")
    .AddCheck<MemoryHealthCheck>("Memory");

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
    options.AddPolicy("create:shoppingCart", policy => policy.Requirements.Add(new HasScopeRequirement("create:shoppingCart", AppConfiguration.Configuration["AppConfiguration:Authentication:Authority"].ToString())));
    options.AddPolicy("read:shopppingCart", policy => policy.Requirements.Add(new HasScopeRequirement("read:shopppingCart", AppConfiguration.Configuration["AppConfiguration:Authentication:Authority"].ToString())));
    options.AddPolicy("update:shoppingCart", policy => policy.Requirements.Add(new HasScopeRequirement("update:shoppingCart", AppConfiguration.Configuration["AppConfiguration:Authentication:Authority"].ToString())));
    options.AddPolicy("delete:shoppingCart", policy => policy.Requirements.Add(new HasScopeRequirement("delete:shoppingCart", AppConfiguration.Configuration["AppConfiguration:Authentication:Authority"].ToString())));
});
builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

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

app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
