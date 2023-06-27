using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using Workspace.BLL.Implementions;
using Workspace.BLL.Interfaces;

namespace Workspace.WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавить сервис авторизации в контейнер.
        /// </summary>
        /// <param name="serviceCollection">Сервисы.</param>
        public static void ConfigureAuthentication(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = ".WebApi";
                    options.Events.OnRedirectToLogin = (context) =>
                    {
                        context.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    };
                });
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Workspace API",
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename), includeControllerXmlComments: true);

                options.OperationFilter<SecurityRequirementsOperationFilter>();
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
            });
        }

        public static void ConfigureManagers(this IServiceCollection services)
        {
            services.AddScoped<IUserManager, UserManager>();
        }
    }
}
