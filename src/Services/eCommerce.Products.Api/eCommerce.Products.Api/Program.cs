using eCommerce.Commons.HealthChecks;
using eCommerce.Commons.Objects.Responses;
using eCommerce.Commons.Objects.Responses.HealthCheck;
using eCommerce.Commons.Security;
using eCommerce.Products.Infraestructure.Contexts.DbProduct;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Products.Core.Config;
using Products.Core.Contracts.Repositories;
using Products.Core.Contracts.Services;
using Products.Core.Helpers.BadRequests;
using Products.Core.Helpers.Log;
using Products.Core.Helpers.Mappers;
using Products.Core.Helpers.ProductReview;
using Products.Infraestructure.DbHelpers;
using Products.Infraestructure.Models.UnitOfWorks;
using Products.Infraestructure.Repositories;
using Products.Infraestructure.Services;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System.Net;
using System.Reflection;
using System.Text;
using WebApiProducts.Config;

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

builder.Services.AddSwaggerGen(c=>
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


builder.Services.AddDbContext<DbProductsContext>(
    options => options.UseSqlServer(AppConfiguration.Configuration["AppConfiguration:DataBases:DbProducts:ConnectionString"].ToString()));

builder.Services.AddScoped<IProductProviderRepository, ProductProviderRepository>();
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddScoped<IProductReviewRepository, ProductReviewRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IDbProductHelper, DbProductHelper>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IProductReviewsHelper, ProductReviewsHelper>();
builder.Services.AddSingleton<ILogHelper, LogHelper>();
builder.Services.AddScoped<IMapperHelper, MapperHelper>();
builder.Services.AddScoped<IProductServices, ProductServices>();

builder.Services.AddHealthChecks()
    .AddSqlServer(
        connectionString: AppConfiguration.Configuration["AppConfiguration:DataBases:DbProducts:ConnectionString"].ToString(),
        healthQuery: "SELECT 1;",
        name: "SqlServerContext")
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
    options.AddPolicy("create:review", policy => policy.Requirements.Add(new HasScopeRequirement("create:review", AppConfiguration.Configuration["AppConfiguration:Authentication:Authority"].ToString())));
    options.AddPolicy("read:review", policy => policy.Requirements.Add(new HasScopeRequirement("read:review", AppConfiguration.Configuration["AppConfiguration:Authentication:Authority"].ToString())));
    options.AddPolicy("read:products", policy => policy.Requirements.Add(new HasScopeRequirement("read:products", AppConfiguration.Configuration["AppConfiguration:Authentication:Authority"].ToString())));
    options.AddPolicy("read:categories", policy => policy.Requirements.Add(new HasScopeRequirement("read:categories", AppConfiguration.Configuration["AppConfiguration:Authentication:Authority"].ToString())));
    options.AddPolicy("read:provider", policy => policy.Requirements.Add(new HasScopeRequirement("read:provider", AppConfiguration.Configuration["AppConfiguration:Authentication:Authority"].ToString())));
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

    if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
    {
        await context.Response.WriteAsync("Token Validation Has Failed. Request Access Denied");
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
