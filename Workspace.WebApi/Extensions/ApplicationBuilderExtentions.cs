using Workspace.WebApi.Middleware;

namespace Workspace.WebApi.Extensions
{
    public static class ApplicationBuilderExtentions
    {
        public static IApplicationBuilder AddExceptionMiddleware(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<ExceptionMiddleware>();

            return applicationBuilder;
        }
    }
}
