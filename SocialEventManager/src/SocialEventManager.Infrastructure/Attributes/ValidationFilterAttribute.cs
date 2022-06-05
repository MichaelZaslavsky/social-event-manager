using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SocialEventManager.Shared.Constants.Validations;

namespace SocialEventManager.Infrastructure.Attributes;

public class ValidationFilterAttribute : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.ActionArguments == null)
        {
            context.Result = new BadRequestObjectResult(ValidationConstants.ObjectIsNull);
            return;
        }

        if (!context.ModelState.IsValid)
        {
            context.Result = new UnprocessableEntityObjectResult(context.ModelState);
        }

        await next();
    }
}
