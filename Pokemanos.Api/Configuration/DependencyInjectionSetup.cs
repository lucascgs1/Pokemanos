using Microsoft.Extensions.DependencyInjection;
using System;
using Pokemanos.Data;
using Pokemanos.Data.Interfaces;
using Pokemanos.Services;
using Pokemanos.Services.Interfaces;

namespace Pokemanos.Api.Configuration
{
    public static class DependencyInjectionSetup
    {
        public static void RegisterServices(this IServiceCollection services)
        {

            if (services == null)
                throw new ArgumentNullException(nameof(services));

            #region repositorios
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ICodigoSegurancaRepository, CodigoSegurancaRepository>();
            #endregion

            #region servicos
            services.AddTransient<IUsuarioServices, UsuarioServices>();
            #endregion
        }

    }
}
