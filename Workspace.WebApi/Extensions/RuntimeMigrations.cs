using Microsoft.EntityFrameworkCore;
using Npgsql;
using Workspace.DAL;

namespace Workspace.WebApi.Extensions
{
    public static class RuntimeMigrations
    {
        /// <summary>
        /// Применить миграцию.
        /// </summary>
        /// <param name="serviceProvider">Service provider.</param>
        public static void Migrate(IServiceProvider serviceProvider)
        {
            serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

            var appContextService = serviceProvider.GetRequiredService<DataContext>();

            appContextService.Database.Migrate();

            using var connection = (NpgsqlConnection)appContextService.Database.GetDbConnection();
            connection.Open();
            connection.ReloadTypes();
        }
    }
}
