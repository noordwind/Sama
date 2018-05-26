using System.Reflection;
using Autofac;
using Microsoft.Extensions.Configuration;
using Sama.Core.Domain.Identity;
using Sama.Core.Domain.Ngos;
using Sama.Infrastructure.Maps;
using Sama.Infrastructure.Mongo;
using Sama.Infrastructure.Options;

namespace Sama.Infrastructure
{
    public static class InfrastructureContainer
    {
        public static void Load(ContainerBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();  
            builder.AddMongoDB();
            builder.AddMongoDBRepository<Ngo>("Ngos");
            builder.AddMongoDBRepository<RefreshToken>("RefreshTokens");
            builder.AddMongoDBRepository<User>("Users");
            builder.Register(ctx => ctx.Resolve<IConfiguration>().GetOptions<LocationOptions>("location"))
                .SingleInstance();
        }        
    }
}