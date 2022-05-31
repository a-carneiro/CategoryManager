using CategoryManager.Interface.Repository;
using CategoryManager.Interface.Service;
using CategoryManager.Repository.Context;
using CategoryManager.Repository.Repository;
using CategoryManager.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CategoryManager.DependencyInjection
{
    public static class DependencyInjectionContainer
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CategoryManagerContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure();
                });
            });

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();

        }
    }
}
