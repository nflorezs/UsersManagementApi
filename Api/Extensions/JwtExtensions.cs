using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Services;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace WebApplication1
{
    internal static class JwtExtensions
    {
        /// <summary>
        /// Add json web token documentation
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        internal static IServiceCollection AddJwtDocumentation(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                    ValidateIssuer = false,
                    ValidIssuer = "Tech",
                    ValidAudience = "Tech",
                    ValidateAudience = false,
                };
            });
            return services;
        }

        /// <summary>
        /// Uso de json web token
        /// </summary>
        /// <param name="app">Constructor de apliacion</param>
        /// <returns>Constructor de aplicacion</returns>
        internal static IApplicationBuilder UseJwtAuthentication(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            return app;
        }
    }
}
