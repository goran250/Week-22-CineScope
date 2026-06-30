using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace CineScope
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("CineScopeDbConnString") ?? 
                                   throw new InvalidOperationException("Connection string 'CineScopeDbConnString' not found.");

            builder.Services.AddDbContext<CineScopeDbContext>(options => options.UseSqlServer(connectionString));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();    

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            // This method only runs once when the database is empty. Ej testad
            // var result = RunMigrationsAndAddData(app);

            app.Run();
        }

        private static async Task<IActionResult> RunMigrationsAndAddData(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var appDbContext = scope.ServiceProvider.GetRequiredService<CineScopeDbContext>();

                // appDbContext.Database.Migrate();

                if (!appDbContext.Movies.Any())
                {
                    var scriptPath = "./Arkiv/CineScopeDB.data.sql";

                    var sqlScript = File.ReadAllText(scriptPath);

                    using (var connection = appDbContext.Database.GetDbConnection())
                    {
                        connection.Open();
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = sqlScript;
                            await command.ExecuteNonQueryAsync();
                        }
                    }
                }
            }

            return null;
        }
    }
}
