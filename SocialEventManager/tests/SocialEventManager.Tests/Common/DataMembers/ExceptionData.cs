using System.Net;
using SocialEventManager.Shared.Common.Constants;
using SocialEventManager.Shared.Exceptions;

namespace SocialEventManager.Tests.Common.DataMembers
{
    public static class ExceptionData
    {
        public static IEnumerable<object[]> ExceptionDataForHttpStatusAndTitle
        {
            get
            {
                yield return new object[] { new NotFoundException(), (HttpStatusCode.NotFound, ExceptionConstants.NotFound) };
                yield return new object[] { new BadRequestException(), (HttpStatusCode.BadRequest, ExceptionConstants.BadRequest) };
                yield return new object[] { new ValidationException(), (HttpStatusCode.UnprocessableEntity, ExceptionConstants.UnprocessableEntity) };
                yield return new object[] { null, (HttpStatusCode.InternalServerError, ExceptionConstants.InternalServerError) };
                yield return new object[] { new NullReferenceException(), (HttpStatusCode.InternalServerError, ExceptionConstants.InternalServerError) };
                yield return new object[] { new ArgumentNullException(), (HttpStatusCode.InternalServerError, ExceptionConstants.InternalServerError) };
                yield return new object[] { new ArgumentException(), (HttpStatusCode.InternalServerError, ExceptionConstants.InternalServerError) };
                yield return new object[] { new Exception(), (HttpStatusCode.InternalServerError, ExceptionConstants.InternalServerError) };
            }
        }

        public static IEnumerable<object[]> CriticalExceptionsData
        {
            get
            {
                yield return new object[] { new OutOfMemoryException(), true };
                yield return new object[] { new AppDomainUnloadedException(), true };
                yield return new object[] { new BadImageFormatException(), true };
                yield return new object[] { new CannotUnloadAppDomainException(), true };
                yield return new object[] { new InvalidProgramException(), true };
                yield return new object[] { new Exception(ExceptionConstants.CannotOpenDatabase), true };
                yield return new object[] { new Exception(ExceptionConstants.ANetworkRelated), true };
                yield return new object[] { null, false };
                yield return new object[] { new NullReferenceException(), false };
                yield return new object[] { new ArgumentNullException(), false };
                yield return new object[] { new ArgumentException(), false };
            }
        }
    }
}
