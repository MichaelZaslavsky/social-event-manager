using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Extensions;

namespace SocialEventManager.Tests.Common.DependencyInjection
{
    public static class RepositoryRegistrationCollectionExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            Assembly dalAssembly = Assembly.Load($"{nameof(SocialEventManager)}.{nameof(DAL)}");
            IEnumerable<Type> repositories = dalAssembly.GetTypes()
                .Where(t => t.IsInterface && t.Name.StartsWith(GlobalConstants.InterfacePrefix) && t.Name.EndsWith(GlobalConstants.Repository));

            Assembly testsAssembly = Assembly.Load($"{nameof(SocialEventManager)}.{nameof(Tests)}");
            IDictionary<string, Type> stubsPerTypeName = testsAssembly
                .GetTypes()
                .Where(t => t.IsClass && t.Name.EndsWith(GlobalConstants.RepositoryStub))
                .ToDictionary(t => t.Name.TakeUntilLast(GlobalConstants.RepositoryStub));

            foreach (Type repository in repositories)
            {
                string name = repository.FullName.TakeAfterFirst(GlobalConstants.InterfacePrefix).TakeUntilLast(GlobalConstants.Repository);

                if (stubsPerTypeName.ContainsKey(name))
                {
                    services.AddTransient(repository, stubsPerTypeName[name]);
                }
            }

            return services;
        }
    }
}
