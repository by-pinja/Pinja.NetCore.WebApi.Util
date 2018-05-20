using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Protacon.NetCore.WebApi.Util.RequestLogging
{
    public class RequestLoggerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<RequestLoggerMiddleware> _logger;
        public RequestLoggerMiddleware(RequestDelegate next, ILogger<RequestLoggerMiddleware> logger)
        {
            this.next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var requestBodyStream = new MemoryStream();
            var originalRequestBody = context.Request.Body;

            await context.Request.Body.CopyToAsync(requestBodyStream);
            requestBodyStream.Seek(0, SeekOrigin.Begin);

            var hasJsonContent = context.Request.ContentType != null && context.Request.ContentType.Contains("json");

            var url = UriHelper.GetDisplayUrl(context.Request);
            var requestBodyText = new StreamReader(requestBodyStream).ReadToEnd();

            var messageObjToLog = new
            {
                scheme = context.Request.Scheme,
                host = context.Request.Host,
                path = context.Request.Path,
                queryString = context.Request.Query,
                requestBody = hasJsonContent ? JObject.Parse(requestBodyText ?? "{}") : (object)requestBodyText
            };

            _logger.LogDebug(JsonConvert.SerializeObject(messageObjToLog, Formatting.Indented));

            requestBodyStream.Seek(0, SeekOrigin.Begin);
            context.Request.Body = requestBodyStream;

            await next(context);
            context.Request.Body = originalRequestBody;
        }
    }
}