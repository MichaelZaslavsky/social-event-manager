using AspNetCoreRateLimit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.DependencyInjection;
using SocialEventManager.BLL.Services.Email;
using SocialEventManager.DAL.Infrastructure;
using SocialEventManager.Infrastructure.Auth;
using SocialEventManager.Infrastructure.Email;
using SocialEventManager.Infrastructure.Loggers;
using SocialEventManager.Tests.Common.Constants;
using Xunit;

namespace SocialEventManager.Tests.Common.DataMembers;

internal static class DependencyData
{
    public static TheoryData<IList<(Type ServiceType, Type? ImplementationType, ServiceLifetime Lifetime)>> ValidDependencies =>
        new()
        {
            {
                new List<(Type ServiceType, Type? ImplementationType, ServiceLifetime Lifetime)>
                {
                    (typeof(IUnitOfWork), typeof(UnitOfWork), ServiceLifetime.Transient),
                    (typeof(IEmailProvider), typeof(EmailSmtpProvider), ServiceLifetime.Scoped),
                    (typeof(IEmailRenderer), typeof(RazorEmailRenderer), ServiceLifetime.Scoped),
                    (typeof(IJwtHandler), typeof(JwtHandler), ServiceLifetime.Scoped),
                    (typeof(ISmtpClient), typeof(SmtpClient), ServiceLifetime.Scoped),
                    (typeof(IScopeInformation), typeof(ScopeInformation), ServiceLifetime.Singleton),
                    (typeof(IRateLimitConfiguration), typeof(RateLimitConfiguration), ServiceLifetime.Singleton),
                }
            },
        };

    public static TheoryData<IList<(Type ServiceType, Type? ImplementationType, ServiceLifetime Lifetime)>, string> InvalidDependencies =>
        new()
        {
            {
                new List<(Type ServiceType, Type? ImplementationType, ServiceLifetime Lifetime)>
                {
                    (typeof(IUnitOfWork), typeof(UnitOfWork), ServiceLifetime.Singleton),
                },
                $"""
                {TestConstants.FailedToFindRegisteredServices}{nameof(IUnitOfWork)}|{nameof(UnitOfWork)}|{nameof(ServiceLifetime.Singleton)}

                """
            },
            {
                new List<(Type ServiceType, Type? ImplementationType, ServiceLifetime Lifetime)>
                {
                    (typeof(IUnitOfWork), typeof(UnitOfWork), ServiceLifetime.Singleton),
                    (typeof(IEmailProvider), typeof(EmailSmtpProvider), ServiceLifetime.Transient),
                },
                $"""
                {TestConstants.FailedToFindRegisteredServices}{nameof(IUnitOfWork)}|{nameof(UnitOfWork)}|{nameof(ServiceLifetime.Singleton)}
                {nameof(IEmailProvider)}|{nameof(EmailSmtpProvider)}|{nameof(ServiceLifetime.Transient)}

                """
            },
            {
                new List<(Type ServiceType, Type? ImplementationType, ServiceLifetime Lifetime)>
                {
                    (typeof(IUnitOfWork), typeof(UnitOfWork), ServiceLifetime.Singleton),
                    (typeof(IEmailProvider), typeof(EmailSmtpProvider), ServiceLifetime.Transient),
                    (typeof(IEmailRenderer), typeof(RazorEmailRenderer), ServiceLifetime.Scoped),
                },
                $"""
                {TestConstants.FailedToFindRegisteredServices}{nameof(IUnitOfWork)}|{nameof(UnitOfWork)}|{nameof(ServiceLifetime.Singleton)}
                {nameof(IEmailProvider)}|{nameof(EmailSmtpProvider)}|{nameof(ServiceLifetime.Transient)}

                """
            },
        };
}
