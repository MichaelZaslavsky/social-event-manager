using System.Data.SqlClient;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Exceptions;
using SocialEventManager.Shared.Models.Auth;

namespace SocialEventManager.Tests.IntegrationTests.Fixtures.Fakes;

public sealed class FakeInvalidUserManager : UserManager<ApplicationUser>
{
    public FakeInvalidUserManager()
        : base(
            Mock.Of<IUserStore<ApplicationUser>>(),
            Mock.Of<IOptions<IdentityOptions>>(),
            Mock.Of<IPasswordHasher<ApplicationUser>>(),
            Array.Empty<IUserValidator<ApplicationUser>>(),
            Array.Empty<IPasswordValidator<ApplicationUser>>(),
            Mock.Of<ILookupNormalizer>(),
            Mock.Of<IdentityErrorDescriber>(),
            Mock.Of<IServiceProvider>(),
            Mock.Of<ILogger<UserManager<ApplicationUser>>>())
    {
    }

    // A fake method to test exception handling using the UserManager class.
    public override Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
    {
        throw user.FirstName switch
        {
            nameof(ValidationException) => new ValidationException(ExceptionConstants.BadRequest),
            nameof(BadRequestException) => new BadRequestException(ExceptionConstants.BadRequest),
            nameof(NotFoundException) => new NotFoundException(ExceptionConstants.NotFound),
            nameof(UnprocessableEntityException) => new UnprocessableEntityException(ExceptionConstants.UnprocessableEntity),
            nameof(NullReferenceException) => new NullReferenceException(ExceptionConstants.NullReferenceException),
            nameof(ArgumentNullException) => new ArgumentNullException(ExceptionConstants.ArgumentNullException),
            nameof(ArgumentException) => new ArgumentException(ExceptionConstants.ArgumentException),
            nameof(TimeoutException) => new TimeoutException(ExceptionConstants.TimeoutException),
            nameof(SqlException) => Mock.Of<SqlException>(),
            _ => new Exception(ExceptionConstants.AnErrorOccurred),
        };
    }
}
