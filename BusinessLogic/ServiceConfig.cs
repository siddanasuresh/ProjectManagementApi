using DataAccess;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace BusinessLogic
{
    public static class ServiceConfig    {
     
        public static void Config(IServiceCollection services, IConfiguration config)
        {
            services.AddEntityFrameworkSqlServer().
           AddDbContext<ProjectManagerApiDbContext>(option => option.UseSqlServer(config.GetSection("DatabaseConnection").Value), ServiceLifetime.Scoped);

            services.AddTransient<IParentTaskDetails, ParentTaskDetail>();
            services.AddTransient<ITask, TaskRepository>();
            services.AddTransient<IProject, ProjectRepository>();
            services.AddTransient<IUser, UserRepository>();          
        }
    }
}
