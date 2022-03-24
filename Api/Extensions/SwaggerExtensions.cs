using Microsoft.OpenApi.Models;
using System.Reflection;

namespace WebApplication1.Extensions
{
    internal static class SwaggerExtensions
    {
        internal static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(builder.Configuration.GetSection("VersionApi").Value, GetOpenApiInfo(builder.Configuration));
                options.AddSecurityDefinition(builder.Configuration.GetSection("Jwt:Type").Value, GetOpenApiSecurityScheme(builder.Configuration));
                options.AddSecurityRequirement(GetOpenApiSecurityRequirement());
                var filePath = Path.Combine(System.AppContext.BaseDirectory, "MyApi.xml");
                options.IncludeXmlComments(filePath);
            });

            return services;
        }

        

        internal static OpenApiInfo GetOpenApiInfo(IConfiguration configuration)
        {
            return new OpenApiInfo
            {
                Title = string.Format($"API"),
                Version = configuration["VersionApi"],
                Description = "Api description"
            };
        }
        internal static OpenApiSecurityScheme GetOpenApiSecurityScheme(IConfiguration configuration)
        {
            var securityScheme = new OpenApiSecurityScheme()
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT" // Optional
            };
            return securityScheme;
            //new OpenApiSecurityScheme
            //{
            //    In = ParameterLocation.Header,
            //    Type = SecuritySchemeType.ApiKey,
            //    Scheme = configuration["Jwt:Type"]
            //};
        }
        internal static OpenApiSecurityRequirement GetOpenApiSecurityRequirement()
        {
            return new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    }, new List<string>()
                }
            };
        }
        internal static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
            });
            return app;
        }
    }
}
