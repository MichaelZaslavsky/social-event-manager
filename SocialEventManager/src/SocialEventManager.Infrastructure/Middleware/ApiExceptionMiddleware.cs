using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Tests.Common.Constants;

namespace SocialEventManager.Infrastructure.Middleware
{
    public class ApiExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiExceptionMiddleware> _logger;
        private readonly ApiExceptionOptions _options;

        public ApiExceptionMiddleware(ApiExceptionOptions options, RequestDelegate next, ILogger<ApiExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
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

            LogLevel level = _options.DetermineLogLevel?.Invoke(ex) ?? LogLevel.Error;
            _logger.Log(level, ex, $"{ExceptionConstants.AnErrorOccurred}: {message} -- {error.Id}.");

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
