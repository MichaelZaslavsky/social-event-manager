using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.API.Configurations
{
    /// <summary>
    /// Represents API response type configurations.
    /// </summary>
    public static class ResponseTypeConfigurations
    {
        /// <summary>
        /// Configuring global response type attributes.
        /// </summary>
        /// <param name="config">The options to set its filters.</param>
        public static void ConfigureGlobalResponseTypeAttributes(this MvcOptions config)
        {
            config.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
            config.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status401Unauthorized));
            config.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
            config.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status429TooManyRequests));
            config.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
            config.Filters.Add(new ProducesDefaultResponseTypeAttribute());
            config.Filters.Add(new ProducesAttribute(MediaTypeConstants.ApplicationJson, MediaTypeConstants.ApplicationXml));
        }
    }
}
