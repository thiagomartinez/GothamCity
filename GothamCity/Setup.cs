using GothamCity.Domain.Commands.Cep;
using GothamCity.Domain.Interfaces.Repositories;
using GothamCity.Infra.Repository;
using GothamCity.Infra.Repository.Base;
using GothamCity.Infra.Transactions;
using GothamCity.Security;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GothamCity
{
    public static class Setup
    {
        private const string ISSUER = "c1f51f42";
        private const string AUDIENCE = "c6bbbb645024";

        public static void ConfigureMediatR(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly, typeof(AdicionarCepRequest).GetTypeInfo().Assembly);
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<GothamCityContext, GothamCityContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IRepositoryCep, RepositoryCep>();
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "GothamCity", Version = "v1" });
            });
        }

        public static void ConfigureMVC(this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }
    }
}
