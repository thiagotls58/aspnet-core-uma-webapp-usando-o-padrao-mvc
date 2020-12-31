using Alura.ListaLeitura.App.Logica;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Alura.ListaLeitura.App
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            // serviço de rotas do asp.net core
            services.AddRouting();
            services.AddMvc();
        }

        // IApplicationBuilder é resposável pela configuração do Pipeline da requisição
        public void Configure(IApplicationBuilder app) 
        {
            app.UseMvcWithDefaultRoute();
        }

    }
}