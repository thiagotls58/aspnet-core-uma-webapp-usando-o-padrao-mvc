using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            routeBuilder.MapRoute("livros/paraler", LivrosParaLer);
            routeBuilder.MapRoute("livros/lendo", LivrosLendo);
            routeBuilder.MapRoute("livros/lidos", LivrosLidos);
            routeBuilder.MapRoute("Cadastro/NovoLivro/{nome}/{autor}", NovoLivroParaLer);
            routeBuilder.MapRoute("Livros/Detalhes/{id:int}", ExibeDetalhes);

            var rotas = routeBuilder.Build();

            app.UseRouter(rotas);

        }

        public Task ExibeDetalhes(HttpContext context)
        {
            int id = Convert.ToInt32(context.GetRouteValue("id"));
            var repo = new LivroRepositorioCSV();

            var livro = repo.Todos.First(l => l.Id == id);

            return context.Response.WriteAsync(livro.Detalhes());
        }

        public Task NovoLivroParaLer(HttpContext context)
        {
            var livro = new Livro()
            {
                Titulo = context.GetRouteValue("nome").ToString(),
                Autor = context.GetRouteValue("autor").ToString()
            };
            var repo = new LivroRepositorioCSV();
            repo.Incluir(livro);
            return context.Response.WriteAsync("O livro foi adicionado com sucesso");
        }

        public Task Roteamento(HttpContext contexto)
        {
            var _repo = new LivroRepositorioCSV();
            var caminhosAtendidos = new Dictionary<string, RequestDelegate>
            {
                { "/Livros/ParaLer", LivrosParaLer },
                { "/Livros/Lendo", LivrosLendo },
                { "/Livros/Lidos", LivrosLidos }
            };

            if (caminhosAtendidos.ContainsKey(contexto.Request.Path))
            {
                var metodo = caminhosAtendidos[contexto.Request.Path];
                return metodo.Invoke(contexto);
            }

            contexto.Response.StatusCode = 404;
            return contexto.Response.WriteAsync("Caminho inexistente.");
        }

        public Task LivrosParaLer(HttpContext contexto)
        {
            var _repo = new LivroRepositorioCSV();

            return contexto.Response.WriteAsync(_repo.ParaLer.ToString());
        }

        public Task LivrosLendo(HttpContext contexto)
        {
            var _repo = new LivroRepositorioCSV();

            return contexto.Response.WriteAsync(_repo.Lendo.ToString());
        }

        public Task LivrosLidos(HttpContext contexto)
        {
            var _repo = new LivroRepositorioCSV();

            return contexto.Response.WriteAsync(_repo.Lidos.ToString());
        }

    }
}