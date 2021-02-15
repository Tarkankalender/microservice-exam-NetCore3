using Microsoft.AspNetCore.Http;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam.City.Middleware
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;

        public CorrelationIdMiddleware(RequestDelegate next)
        {

            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            var correlationId = Guid.NewGuid().ToString();

            httpContext.Request.Headers.TryGetValue("CorrelationId", out var values);
            if (values.Any())
            {
                correlationId = values.First();
            }

            if (!httpContext.Response.Headers.ContainsKey("CorrelationId"))
            {
                httpContext.Response.Headers.Add("CorrelationId", correlationId);
            }

            using (LogContext.PushProperty("CorrelationId", correlationId))
            {
                return _next(httpContext);
            }
        }

    }
}
