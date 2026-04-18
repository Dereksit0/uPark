using Microsoft.EntityFrameworkCore;
using UPark.Data.Context;

var optionsBuilder = new DbContextOptionsBuilder<UParkDbContext>();
optionsBuilder.UseSqlite("Data Source=upark.db");

using var context = new UParkDbContext(optionsBuilder.Options);
context.Database.Migrate();

Console.WriteLine("Base de datos creada correctamente.");