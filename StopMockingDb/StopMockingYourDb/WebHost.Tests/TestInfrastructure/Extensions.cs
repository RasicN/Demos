using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebHost.DataAccess;

namespace WebHost.Tests.TestInfrastructure
{
    public static class Extensions
    {
        public const string TestDatabaseName = "TestDatabase";

        public static async Task SetupDatabaseAsync(this ProductDbContext context)
        {
            if (context.Database.GetDbConnection().Database != TestDatabaseName)
                throw new Exception($"Not running against database name `{TestDatabaseName}`. Will not continue");

            await using (var connection = new SqlConnection(context.Database.GetConnectionString().Replace(TestDatabaseName, "master")))
            {
                await connection.OpenAsync();
                var createDbSql = $"CREATE DATABASE {TestDatabaseName}";
                await using (var command = new SqlCommand(createDbSql, connection))
                {
                    await command.ExecuteScalarAsync();
                }
            }
            
            await context.Database.MigrateAsync();
        }

        public static async Task TeardownDatabaseAsync(this ProductDbContext context)
        {
            if (context.Database.GetDbConnection().Database != TestDatabaseName)
                throw new Exception($"Not running against database name `{TestDatabaseName}`. Will not continue");

            await context.Database.EnsureDeletedAsync();
        }
    }
}
