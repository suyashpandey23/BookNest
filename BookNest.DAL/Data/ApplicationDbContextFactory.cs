// csharp
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using BookNest.DAL.Data;

namespace BookNest.DAL.DataAccess.Data;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        // Use the same connection string your app uses (from BookNestWeb/appsettings.Development.json)
        // Replace with environment/config as needed.
        var connectionString = "Server=localhost,1433;Database=BookNestDB;User Id=sa;Password=Admin@123;TrustServerCertificate=True;";

        optionsBuilder.UseSqlServer(connectionString);

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
