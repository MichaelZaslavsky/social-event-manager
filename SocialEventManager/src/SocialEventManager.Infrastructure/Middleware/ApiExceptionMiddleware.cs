using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using Serilog.Events;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Tests.Common.Constants;

namespace SocialEventManager.Infrastructure.Middleware
{
    public class ApiExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ApiExceptionOptions _options;

        public ApiExceptionMiddleware(ApiExceptionOptions options, RequestDelegate next)
        {
            _next = next;
            _options = options;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _options);
            }
        }

        #region Private Methods

        private Task HandleExceptionAsync(HttpContext context, Exception ex, ApiExceptionOptions options)
        {
            (HttpStatusCode httpStatusCode, string title) = ex.ToHttpStatusCodeAndTitle();

            var error = new ApiError
            {
                Id = Guid.NewGuid().ToString(),
                Status = (short)httpStatusCode,
                Title = title,
            };

            options.AddResponseDetails?.Invoke(context, ex, error);

            string message = GetInnermostExceptionMessage(ex);

            LogEventLevel level = _options.DetermineLogLevel?.Invoke(ex) ?? LogEventLevel.Error;
            Log.Write(level, ex, $"{ExceptionConstants.AnErrorOccurred}: {message} -- {error.Id}.");

            string result = JsonConvert.SerializeObject(error);
            context.Response.ContentType = ApiConstants.ApplicationJson;
            context.Response.StatusCode = (int)httpStatusCode;

            return context.Response.WriteAsync(result);
        }

        private string GetInnermostExceptionMessage(Exception ex)
        {
            return ex.InnerException == null
                ? ex.Message
                : GetInnermostExceptionMessage(ex.InnerException);
        }

        #endregion Private Methods
    }
}
