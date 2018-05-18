using System.Reflection;
using Autofac;
using Sama.Core.Domain;
using Sama.Core.Domain.Identity;
using Sama.Core.Domain.Ngos;
using Sama.Infrastructure.Mongo;

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
        }        
    }
}