using Kognit.API.WebApi.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Kognit.API.WebApi.Extensions
{
    public static class AppExtensions
    {
        /// <summary>
        ///     Adiciona endpoints do Swagger
        /// </summary>
        public static void UseSwaggerExtension(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CleanArchitecture.Kognit.API.WebApi");
            });
        }

        /// <summary>
        ///     Adiciona o middleware de tratamento de erros
        /// </summary>
        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}