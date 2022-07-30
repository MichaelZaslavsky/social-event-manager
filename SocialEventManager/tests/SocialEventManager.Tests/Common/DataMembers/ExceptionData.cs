using System.Net;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Exceptions;
using Xunit;

namespace SocialEventManager.Tests.Common.DataMembers;

public static class ExceptionData
{
    public static TheoryData<Exception?, (HttpStatusCode, string)> ExceptionDataForHttpStatusAndTitle =>
        new()
        {
            { new NotFoundException(), (HttpStatusCode.NotFound, ExceptionConstants.NotFound) },
            { new BadRequestException(), (HttpStatusCode.BadRequest, ExceptionConstants.BadRequest) },
            { new ValidationException(), (HttpStatusCode.UnprocessableEntity, ExceptionConstants.UnprocessableEntity) },
            { null, (HttpStatusCode.InternalServerError, ExceptionConstants.InternalServerError) },
            { new NullReferenceException(), (HttpStatusCode.InternalServerError, ExceptionConstants.InternalServerError) },
            { new ArgumentNullException(), (HttpStatusCode.InternalServerError, ExceptionConstants.InternalServerError) },
            { new ArgumentException(), (HttpStatusCode.InternalServerError, ExceptionConstants.InternalServerError) },
            { new Exception(), (HttpStatusCode.InternalServerError, ExceptionConstants.InternalServerError) },
        };

    public static TheoryData<Exception?, bool> CriticalExceptionsData =>
        new()
        {
            { new OutOfMemoryException(), true },
            { new AppDomainUnloadedException(), true },
            { new BadImageFormatException(), true },
            { new CannotUnloadAppDomainException(), true },
            { new InvalidProgramException(), true },
            { new Exception(ExceptionConstants.CannotOpenDatabase), true },
            { new Exception(ExceptionConstants.ANetworkRelated), true },
            { null, false },
            { new NullReferenceException(), false },
            { new ArgumentNullException(), false },
            { new ArgumentException(), false },
        };
}
