using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SocialEventManager.Shared.Constants.Validations;

namespace SocialEventManager.Infrastructure.Attributes;

public sealed class ValidationFilterAttribute : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.ActionArguments is null)
        {
            context.Result = new BadRequestObjectResult(ValidationConstants.ObjectIsNull);
            return;
        }

        if (!context.ModelState.IsValid)
        {
            context.Result = new BadRequestObjectResult(context.ModelState);
        }

        await next();
    }
}
