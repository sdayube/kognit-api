using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Text.Json;

namespace Kognit.API.WebApi.Extensions
{
    public static class ServiceExtensions
    {
        /// <summary>
        ///     Adiciona e configura o Swagger
        /// </summary>
        /// <param name="services"></param>
        public static void AddSwaggerExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Kognit - Teste Backend",
                    Description = "API REST criada em ASP.NET para o teste de backend da Kognit.",
                    Contact = new OpenApiContact
                    {
                        Name = "Silvio Dayube Carigé",
                        Email = "scarige@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/silvio-dayube/"),
                    }
                });
            });
        }

        /// <summary>
        ///    Adiciona os Controllers à service collection e configura o serializador JSON para utilizar camelCase.
        /// </summary>
        public static void AddControllersExtension(this IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                })
                ;
        }

        /// <summary>
        ///     Adiciona o serviço de CORS com as configurações "AllowAll" e "AllowSpecificOrigin".
        /// </summary>
        public static void AddCorsExtension(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });

                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:5172") // URL do FrontEnd em produção
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });
        }

        /// <summary>
        ///     Configura o API Explorer para utilizar o versionamento.
        /// </summary>
        public static void AddVersionedApiExplorerExtension(this IServiceCollection services)
        {
            services.AddVersionedApiExplorer(o =>
            {
                o.GroupNameFormat = "'v'VVV";
                o.SubstituteApiVersionInUrl = true;
            });
        }

        /// <summary>
        ///     Adiciona a versão 1.0 da API como padrão, e a utiliza quando a versão não for especificada na rota.
        /// </summary>
        public static void AddApiVersioningExtension(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
            });
        }
    }
}
