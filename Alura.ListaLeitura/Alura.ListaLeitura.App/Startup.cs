using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app) 
        {
            // IApplicationBuilder é resposável pela configuração do Pipeline da requisição

            app.Run(Roteamento);

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