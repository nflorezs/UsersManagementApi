using Mappers.Users;
using Repositories;
using Services;

namespace WebApplication1.Extensions
{
    public static class DImManager
    {
        internal static IServiceCollection AddServicesDependencies(this IServiceCollection services)
        {
            //Adds mapper
            services.AddAutoMapper(typeof(UsersMapper));

            //Adds core
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITokenClaims, TokenClaims>();

            //Adds repositories
            services.AddTransient<IUserRepository, UserRepository>();


            return services;
        }
    }
}
