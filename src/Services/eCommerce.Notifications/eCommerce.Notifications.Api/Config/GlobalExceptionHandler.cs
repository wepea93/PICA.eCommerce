using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System.Net;
using eCommerce.Notifications.Core.Helpers.Log;
using eCommerce.Commons.Objects.Responses;

namespace eCommerce.Notifications.Api.Config
{
    public static class GlobalExceptionHandler
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogHelper logHelper)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var errorFeature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
                    var ex = errorFeature.Error;
                    var errorMessage = ex.InnerException != null ? ex.InnerException.Message + "/" + ex.Message : ex.Message;
                    var trace = "";

                    var request = GetRequestException(context.Request);

                    object detail;
                    detail = new ServiceResponse<string>("Internal server error", errorMessage);
                    var response = JsonConvert.SerializeObject(detail, Formatting.Indented);

                    var message = "Internal server error: {@error}";
                    var error = new { errorMessage = errorMessage, request = request, response = response };

                    await logHelper.RegisterLog(message, error);

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var jsonResponse = JsonConvert.SerializeObject(detail, Formatting.Indented);
                        await context.Response.WriteAsync(jsonResponse);
                    }
                });
            });
        }

        private static RequestException GetRequestException(HttpRequest request)
        {
            var headers = request.Headers.ToDictionary(x => x.Key, x => x.Value);
            var body = "";

            request.Body.Position = 0;
            using (var mem = new MemoryStream())
            using (var reader = new StreamReader(mem))
            {
                request.Body.CopyTo(mem);
                body = reader.ReadToEnd();
                mem.Seek(0, SeekOrigin.Begin);
                body = reader.ReadToEnd();
            }

            var jsonStr = JsonConvert.SerializeObject(body, Formatting.Indented).ToString();

            var bodyStr = JsonConvert.DeserializeObject<string>(jsonStr);

            return new RequestException
            {
                Host = request.Host.Value,
                Path = request.Path,
                ContentType = request.ContentType,
                Scheme = request.Scheme,
                Method = request.Method,
                Headers = JsonConvert.SerializeObject(headers, Formatting.None).ToString(),
                Body = bodyStr
            };
        }

        private class RequestException
        {
            public string Host { get; set; }
            public string Path { get; set; }
            public string ContentType { get; set; }
            public string Scheme { get; set; }
            public string Headers { get; set; }
            public string Method { get; set; }
            public string Body { get; set; }

            public override string ToString()
            {
                return JsonConvert.SerializeObject(this, Formatting.Indented);
            }

        }
    }
}
