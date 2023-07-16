using Kognit.API.Application.Interfaces;
using Kognit.API.Application.Interfaces.Repositories;
using Kognit.API.Infrastructure.Persistence.Contexts;
using Kognit.API.Infrastructure.Persistence.Repositories;
using Kognit.API.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace Kognit.API.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        /// <summary>
        ///     Adiciona as conexões com o banco de dados.
        /// </summary>
        /// <param name="configuration">
        ///     Objeto de configuração.
        /// </param>
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("KognitApiInMemDatabase"));
            }
            else
            {
                string dataDirectoryPath = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
                var connectionString = configuration.GetConnectionString("DefaultConnection");

                if (connectionString.Contains("%CONTENTROOTPATH%"))
                {
                    connectionString = connectionString.Replace("%CONTENTROOTPATH%", dataDirectoryPath);
                }

                services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   connectionString,
                   b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }

            #region Repositories

            services.AddTransient(typeof(IRepositoryAsync<>), typeof(BaseRepositoryAsync<>));
            services.AddTransient<IUserRepositoryAsync, UserRepositoryAsync>();
            services.AddTransient<IWalletRepositoryAsync, WalletRepositoryAsync>();

            #endregion Repositories
        }
    }
}