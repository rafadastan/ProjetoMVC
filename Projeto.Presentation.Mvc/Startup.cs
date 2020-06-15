using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Projeto.Repository.SqlServer.Entities;
using Projeto.Repository.SqlServer.Repositories;

namespace Projeto.Presentation.Mvc
{
    public class Startup
    {
        //construtor
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        //prop + 2x[tab]
        //componente do NET CORE capaz de ler o arquivo appsettings.json
        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Criar uma configuração para habilitar o uso de Views e Controllers no projeto
            services.AddControllersWithViews();

            //Habilitar o projeto para usar cookies
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

            //obter a connectionstring no arquivo appsettings.json
            var connectionString = Configuration.GetConnectionString("Projeto");

            //inicializar as classes do repositorio com a connectionstring
            services.AddTransient(s => new FuncionarioRepository(connectionString));
            services.AddTransient(s => new DependenteRepository(connectionString));
            services.AddTransient(s => new UsuarioRepository(connectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Habilitar o uso de autenticação com o cookie
            app.UseCookiePolicy();
            app.UseAuthentication();

            //configuração para habilitar o uso da pasta /wwwroot
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //mapear a página inicial do projeto (default)
                endpoints.MapControllerRoute(
                    name : "default",
                    pattern : "{controller=Account}/{action=Login}/{id?}"
                );
            });
        }
    }
}
