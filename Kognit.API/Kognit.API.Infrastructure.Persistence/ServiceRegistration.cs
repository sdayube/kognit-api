using Kognit.API.Application.Interfaces;
using Kognit.API.Application.Interfaces.Repositories;
using Kognit.API.Infrastructure.Persistence.Contexts;
using Kognit.API.Infrastructure.Persistence.Repositories;
using Kognit.API.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kognit.API.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("DefaultConnection"),
                   b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }

            #region Repositories

            services.AddTransient(typeof(IRepositoryAsync<>), typeof(BaseRepositoryAsync<>));
            services.AddTransient<IUserRepositoryAsync, UserRepositoryAsync>();
            services.AddTransient<IEmployeeRepositoryAsync, EmployeeRepositoryAsync>();

            #endregion Repositories
        }
    }
}