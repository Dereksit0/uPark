using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using UPark.Data.Context;

namespace UPark.Migrations;

public class UParkDbContextFactory : IDesignTimeDbContextFactory<UParkDbContext>
{
    public UParkDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<UParkDbContext>();
        optionsBuilder.UseSqlite(
            "Data Source=upark.db",
            b => b.MigrationsAssembly("UPark.Migrations")
        );

        return new UParkDbContext(optionsBuilder.Options);
    }
}