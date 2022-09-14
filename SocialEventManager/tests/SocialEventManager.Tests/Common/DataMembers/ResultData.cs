using FluentValidation;
using LanguageExt.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialEventManager.Shared.Exceptions;
using SocialEventManager.Tests.Common.Constants;
using Xunit;

namespace SocialEventManager.Tests.Common.DataMembers;

internal static class ResultData
{
    public static TheoryData<Result<object>, Type, int> ResultDataWithExceptions =>
        new()
        {
            { new Result<object>(new BadRequestException()), typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest },
            { new Result<object>(new ValidationException(TestConstants.SomeText)), typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest },
            { new Result<object>(new UnauthorizedAccessException()), typeof(UnauthorizedObjectResult), StatusCodes.Status401Unauthorized },
            { new Result<object>(new NotFoundException()), typeof(NotFoundObjectResult), StatusCodes.Status404NotFound },
            { new Result<object>(new UnprocessableEntityException()), typeof(UnprocessableEntityObjectResult), StatusCodes.Status422UnprocessableEntity },
        };
}
