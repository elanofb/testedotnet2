using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TesteElano.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;
using teste_k2_elano_barreto.Models;

namespace TesteElano
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //   This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddRazorPages();

            services.AddDbContext<DB_A46567_cotacaoContext>(item => item.UseSqlServer(Configuration.GetConnectionString("K2DBConnection")));
            services.AddScoped<IFilmeRepository, FilmeRepository>();

            #region CONFIGURAÇÕES DO TOKEN


            #endregion

            #region SWAGGER
            services.AddSwaggerGen(c => {

                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "LoK2 Filmes",
                        Version = "v1",
                        Description = "Api REST para aluguel de filmes.",
                        Contact = new OpenApiContact
                        {
                            Name = "Elano Barreto",
                            //Url = "https://github.com/elanofb"
                        }
                    });
            });
            #endregion

            #region CORS
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                       builder =>
                       builder.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader());
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region SWAGGER
            // Middlewares para uso do Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LoK2 Filmes - V1");
            });

            #endregion

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //CORS
            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
