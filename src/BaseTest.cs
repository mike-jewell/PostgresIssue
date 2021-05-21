using Docker.DotNet;
using Docker.DotNet.Models;
using Example.Docker;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

[assembly: CollectionBehavior(CollectionBehavior.CollectionPerAssembly)]
namespace Example
{
    public class BaseTest : IDisposable
    {
        protected readonly IServiceProvider ServiceProvider;

        protected BaseTest(ITestOutputHelper outputHelper)
        {
            const string connStr = "host=localhost;port=5432;database=test_db;user id=postgres;password=password1;";

            DockerExtentions.StartDockerServicesAsync(new List<Func<DockerClient, Task<ContainerListResponse>>>
            {
                    Postgres.StartPostgres
            }).Wait();

            if (ServiceProvider == null)
            {
                var serviceCollection = new ServiceCollection();

                serviceCollection.AddDbContext<EfContext>(options => options.UseNpgsql(connStr));

                ServiceProvider = serviceCollection.BuildServiceProvider();

                using (var scope = ServiceProvider.CreateScope())
                {
                    var efContext = scope.ServiceProvider.GetRequiredService<EfContext>();

                    // On the second run, this is stil true even though its a brand new database.
                    var dbExists = (efContext.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists();

                    // On the second run this fails, Npgsql.NpgsqlException : Exception while reading from stream
                    // Assume its because its trying to read from a database that doesn't exist
                    efContext.Database.Migrate();
                }
            }
        }

        public void Dispose()
        {
            DockerExtentions.RemoveDockerServicesAsync(true).Wait();
        }
    }
}
