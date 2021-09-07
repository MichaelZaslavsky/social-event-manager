using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SocialEventManager.API.Configurations
{
    public static class ResponseTypeConfigurations
    {
        public static void ConfigureGlobalResponseTypeAttributes(this MvcOptions config)
        {
            config.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
            config.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status401Unauthorized));
            config.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
            config.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status429TooManyRequests));
            config.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
            config.Filters.Add(new ProducesDefaultResponseTypeAttribute());
        }
    }
}
