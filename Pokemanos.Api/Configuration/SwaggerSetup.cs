using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Pokemanos.Api.Configuration
{
    public static class SwaggerSetup
    {
        public static void AddSwaggerSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc("v1",
                        new OpenApiInfo
                        {
                            Title = "Pokemanos",
                            Version = "v1",
                            Description = "Api criada para exemplificar crud",
                            Contact = new OpenApiContact
                            {
                                Name = "Lucas Coutinho",
                                Url = new Uri("https://github.com/lucascgs1")
                            }
                        });

                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                });

        }

        public static void UseSwaggerSetup(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pokemanos");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
