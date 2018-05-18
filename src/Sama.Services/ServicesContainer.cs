using System.Reflection;
using Autofac;
using Sama.Core.Domain;
using Sama.Core.Domain.Identity;
using Sama.Core.Domain.Identity.Factories;
using Sama.Core.Domain.Identity.Specifications;
using Sama.Core.Types;
using Microsoft.AspNetCore.Identity;
using Sama.Services.Data;
using Sama.Infrastructure.Mongo;

namespace Sama.Services
{
    public static class ServicesContainer
    {
        public static void Load(ContainerBuilder builder)
        {
            var servicesAssembly = Assembly.GetExecutingAssembly();
            var coreAssembly = Assembly.GetAssembly(typeof(IEntity));
            builder.RegisterAssemblyTypes(servicesAssembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(coreAssembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterType<PasswordHasher<User>>().As<IPasswordHasher<User>>();
            builder.RegisterType<DataInitializer>().As<IMongoDbSeeder>();
        }
    }
}