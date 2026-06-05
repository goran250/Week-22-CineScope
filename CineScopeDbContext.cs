using CineScope.Models;
using Microsoft.EntityFrameworkCore;

public class CineScopeDbContext(DbContextOptions<CineScopeDbContext> options) : DbContext(options)
{
    public DbSet<CineScope.Models.Movie> Movies { get; set; } = default!;
    public DbSet<CineScope.Models.Actor> Actors { get; set; }
    public DbSet<CineScope.Models.ActorMovie> ActorsMovies{ get; set; }
}
