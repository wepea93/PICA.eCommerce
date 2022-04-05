using Authorizer.Core.Contracts.Repositories;
using Authorizer.Core.Contracts.Services;
using Authorizer.Infraestructure.Models.DbAuthentication;
using Authorizer.Infraestructure.Models.UnitOfWorks;
using Authorizer.Infraestructure.Repositories;
using Authorizer.Infraestructure.Services;
using Autorizer.Core.Config;
using Autorizer.Core.HealthChecks;
using Autorizer.Core.Helpers.BadRequests;
using Autorizer.Core.Helpers.Log;
using Autorizer.Core.Helpers.Mappers;
using Autorizer.Core.Objects.Responses;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System.Reflection;
using System.Text;
using WebApiAutorizer.Config;


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
});

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


builder.Services.AddDbContext<DbAuthenticationContext>(
    options => options.UseSqlServer(AppConfiguration.Configuration["AppConfiguration:DataBases:AuthenticationDatabase:ConnectionString"].ToString()));

builder.Services.AddScoped<IAuthorizeRepository, AuthorizeRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddSingleton<ILogHelper, LogHelper>();
builder.Services.AddScoped<IMapperHelper, MapperHelper>();
builder.Services.AddScoped<IAuthorizeService, AuthorizeService>();

builder.Services.AddHealthChecks()
    .AddSqlServer(
        connectionString: AppConfiguration.Configuration["AppConfiguration:DataBases:AuthenticationDatabase:ConnectionString"].ToString(),
        healthQuery: "SELECT 1;",
        name: "SqlServerContext")
    .AddCheck<MemoryHealthCheck>("Memory");

builder.Services.Configure<IISOptions>(options =>
{
    options.ForwardClientCertificate = false;
});


var app = builder.Build();

//app.MapHealthChecks("/healthz");

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

app.UseAuthorization();

app.MapControllers();

app.Run();
