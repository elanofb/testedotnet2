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
using TesteElano.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TesteElano.Util;
//using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;

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

            services.AddDbContext<TesteElanoContext>(item => item.UseSqlServer(Configuration.GetConnectionString("LubyDBConnection")));
            services.AddScoped<IProjetoRepository, ProjetoRepository>();
            services.AddScoped<ILancamentoRepository, LancamentoRepository>();
            services.AddScoped<IDesenvolvedorRepository, DesenvolvedorRepository>();

            #region CONFIGURAÇÕES DO TOKEN

            
            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                Configuration.GetSection("TokenConfigurations"))
                    .Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);


            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
                                            {
                                                var paramsValidation = bearerOptions.TokenValidationParameters;
                                                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                                                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                                                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                                                // Valida a assinatura de um token recebido
                                                paramsValidation.ValidateIssuerSigningKey = true;

                                                // Verifica se um token recebido ainda é válido
                                                paramsValidation.ValidateLifetime = true;

                                                // Tempo de tolerância para a expiração de um token (utilizado
                                                // caso haja problemas de sincronismo de horário entre diferentes
                                                // computadores envolvidos no processo de comunicação)
                                                paramsValidation.ClockSkew = TimeSpan.Zero;
                                            });

            // Ativa o uso do token como forma de autorizar o acesso a recursos do projeto
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            #endregion

            #region SWAGGER
            services.AddSwaggerGen(c => {

                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Lançamento de Horas",
                        Version = "v1",
                        Description = "Api REST para lançamento de Horas.",
                        Contact = new OpenApiContact
                        {
                            Name = "Elano Barreto",
                            //Url = "https://github.com/elanofb"
                        }
                    });
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lançamento de Horas V1");
            });

            #endregion

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

          
        }
    }
}
