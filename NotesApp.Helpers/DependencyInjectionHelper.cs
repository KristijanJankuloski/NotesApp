using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NotesApp.DataAccess.Context;
using NotesApp.DataAccess.Repositories.Implementations;
using NotesApp.DataAccess.Repositories.Interfaces;
using NotesApp.Services.Implementaitons;
using NotesApp.Services.Interfaces;

namespace NotesApp.Helpers
{
    public static class DependencyInjectionHelper
    { 
        public static void InjectDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<NotesDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }

        public static void InjectRepositories(this IServiceCollection services)
        {
            services.AddScoped<INoteRepository, NoteRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        public static void InjectServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<INoteService, NoteService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}