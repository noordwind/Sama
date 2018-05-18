using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Sama.Api.Framework;
using Sama.Infrastructure;
using Sama.Infrastructure.Authentication;
using Sama.Infrastructure.Mvc;
using Sama.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sama.Infrastructure.Mongo;

namespace Sama.Api
{
    public class Startup
    {
        private static readonly string[] Headers = new []{ "x-total-count" };
        private static readonly string CorsPolicy = "CorsPolicy";
        public IConfiguration Configuration { get; }
        public IContainer Container { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddDefaultJsonOptions();
            services.AddJwt();
            services.AddAuthorization(x => x.AddPolicy("admin", p => p.RequireRole("admin")));
            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicy, cors => 
                        cors.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials()
                            .WithExposedHeaders(Headers));
            });
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                    .AsImplementedInterfaces();
            InfrastructureContainer.Load(builder);
            ServicesContainer.Load(builder);
            builder.Populate(services);
            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IApplicationLifetime applicationLifetime, ILoggerFactory loggerFactory,
            IMongoDbSeeder dbSeeder)
        {
            if (env.IsDevelopment() || env.EnvironmentName == "local")
            {
                app.UseDeveloperExceptionPage();
            }
            loggerFactory.AddConsole().AddDebug();
            app.UseAuthentication();
            app.UseCors(CorsPolicy);
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseMvc();
            dbSeeder.SeedAsync();
            applicationLifetime.ApplicationStopped.Register(() => Container.Dispose());
        }
    }
}
