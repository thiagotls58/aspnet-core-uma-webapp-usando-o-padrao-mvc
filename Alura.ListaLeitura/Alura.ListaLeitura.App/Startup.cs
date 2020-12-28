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
        }

        // IApplicationBuilder é resposável pela configuração do Pipeline da requisição
        public void Configure(IApplicationBuilder app) 
        {
            // definindo as rotas
            var routeBuilder = new RouteBuilder(app);
            routeBuilder.MapRoute("livros/paraler", LivrosLogica.LivrosParaLer);
            routeBuilder.MapRoute("livros/lendo", LivrosLogica.LivrosLendo);
            routeBuilder.MapRoute("livros/lidos", LivrosLogica.LivrosLidos);
            routeBuilder.MapRoute("Livros/Detalhes/{id:int}", LivrosLogica.ExibeDetalhes);
            routeBuilder.MapRoute("Cadastro/NovoLivro/{nome}/{autor}", CadastroLogica.NovoLivroParaLer);
            routeBuilder.MapRoute("Cadastro/NovoLivro", CadastroLogica.ExibeFormulario);
            routeBuilder.MapRoute("Cadastro/Incluir", CadastroLogica.ProcessaFormulario);

            var rotas = routeBuilder.Build();

            app.UseRouter(rotas);

        }

    }
}