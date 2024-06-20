using System;
using System.Data;
using System.Reflection;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace HotLineAssistant.Testing.Functional
{
    public abstract class DbContextTestFixture<TDbContext> : IDisposable where TDbContext : DbContext
    {
        public readonly DbContextOptions<TDbContext> Options;
        public readonly Mock<IMediator> Mediator;

        public string ConnectionString { get; }

        protected DbContextTestFixture(string connectionString)
        {
            ConnectionString = connectionString;

            Options = SetupDbContextOptions();

            Mediator = new Mock<IMediator>();

            SetupDatabase();
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public abstract TDbContext CreateContext();

        protected DbContextOptions<TDbContext> SetupDbContextOptions()
        {
            var migrationAssembly = typeof(TDbContext).GetTypeInfo().Assembly.GetName().Name;

            return new DbContextOptionsBuilder<TDbContext>()
                .UseSqlServer(
                    ConnectionString,
                    sqlOptions => {sqlOptions
                        .MigrationsAssembly(migrationAssembly)
                        .EnableRetryOnFailure(10);
                    })
                .Options; 
        }
        
        protected void SetupDatabase()
        {
            using (var context = CreateContext())
            {
                context.Database.Migrate();

                SeedDatabase(context);
            }
        }

        protected virtual void SeedDatabase(TDbContext context)
        {
        }

        public virtual void Dispose()
        {
            CreateContext().Database.EnsureDeleted();
        } 
    }
}