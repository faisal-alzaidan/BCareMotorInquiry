using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Yaqeen.Infrastructure.Persistence
{
    public class YaqeenDbContextFactory : IDesignTimeDbContextFactory<YaqeenDbContext>
    {
        public YaqeenDbContext CreateDbContext(string[] args)
        {
            
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../Yaqeen.API");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddUserSecrets("37b9061d-e04b-4ea8-b8ad-cfbb9c579b08")
                .Build();

            var builder = new DbContextOptionsBuilder<YaqeenDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseSqlServer(connectionString);

            return new YaqeenDbContext(builder.Options);
        }
    }
}