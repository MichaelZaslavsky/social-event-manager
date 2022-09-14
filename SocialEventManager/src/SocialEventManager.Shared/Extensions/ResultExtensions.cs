using FluentValidation;
using LanguageExt.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialEventManager.Shared.Exceptions;

namespace SocialEventManager.Shared.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToOk<TResult, TContract>(this Result<TResult> result, Func<TResult, TContract> mapper)
    {
        return result.Match(
            obj =>
            {
                TContract response = mapper(obj);
                return new OkObjectResult(response);
            }, exception => GetResult(exception));
    }

    private static IActionResult GetResult(Exception exception)
    {
        return exception switch
        {
            BadRequestException or ValidationException => new BadRequestObjectResult(exception.Message),
            UnauthorizedAccessException ex => new UnauthorizedObjectResult(ex.Message),
            NotFoundException ex => new NotFoundObjectResult(ex.Message),
            UnprocessableEntityException ex => new UnprocessableEntityObjectResult(ex.Message),
            _ => new StatusCodeResult(StatusCodes.Status500InternalServerError),
        };
    }
}
