using CineScope.Models;
using CineScope.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace CineScope.Controllers
{
    public class HomeController : Controller
    {
        private readonly CineScopeDbContext cineScopeDbContext;

        public HomeController(CineScopeDbContext context)
        {
            cineScopeDbContext = context;
        }

        public IActionResult Index(string? searchValue, string? genre)
        {
            // List<Movie> movies = cineScopeDbContext.Movies.ToList();
            List<Movie> movies;
            List<SelectListItem> genrerListItems = new List<SelectListItem>();

            movies =  cineScopeDbContext.Movies.ToList();

            genrerListItems.Add(new SelectListItem { Value = "Alla genrer", Text = "Alla genrer" });

            foreach (Movie movie in movies)
            {
                // Ser till så att gener är unika.
                if (!genrerListItems.Any(g => g.Value == movie.Genre)) { 
                    genrerListItems.Add(new SelectListItem { Value = movie.Genre, Text = movie.Genre });
                }
            }

            if (!string.IsNullOrEmpty(searchValue))
            {
                movies = movies.Where(m => m.Title.Contains(searchValue)).ToList();
            }

            movies = movies.OrderBy(m => m.Title).ToList();


            if (!string.IsNullOrEmpty(genre) && genre != "Alla genrer")
            {
                movies = movies.Where(m => m.Genre == genre).ToList();
            }

            List<Movie> topMovies = GetTopMovies(movies);

            HomeIndex homeIndex = new HomeIndex(movies, topMovies, genrerListItems);
            homeIndex.specialString = "<div id='frame1' class='hidden visible'>";

            return View(homeIndex);
        }

        public async Task<IActionResult> MovieDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = cineScopeDbContext.Movies.Find(id);

            if (movie == null)
            {
                return NotFound();
            }

            List<int> actorIds = cineScopeDbContext.ActorsMovies.Where(am => am.MoviesId == movie.Id).Select(am => am.ActorsId).ToList();
            List<Actor> actors = cineScopeDbContext.Actors.Where(a => actorIds.Contains(a.Id)).ToList();

            HomeMovieDetails homeMovieDetails = new HomeMovieDetails(movie, actors);

            return View(homeMovieDetails);
        }

        
        // GET: ACTORS
        public async Task<IActionResult> AllActors()
        {
            return View(await cineScopeDbContext.Actors.ToListAsync());
        }


        // GET: ACTORS/Details/5
        public async Task<IActionResult> ActorDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await cineScopeDbContext.Actors.FirstOrDefaultAsync(m => m.Id == id);
            if (actor == null)
            {
                return NotFound();
            }

            List<int> movieIds = await cineScopeDbContext.ActorsMovies.Where(am => am.ActorsId == actor.Id).
                                       Select(am => am.MoviesId).ToListAsync();

            actor.Movies = await cineScopeDbContext.Movies.Where(m => movieIds.Contains(m.Id)).ToListAsync();

            actor.Movies = actor.Movies.OrderBy(m => m.ReleaseDate).ToList();

            return View(actor);
        }

        public IActionResult ContactUs()
        {
            return View(ContactUs);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private List<Movie> GetTopMovies(List<Movie> movies)
        {
            List<Movie> topMovies = new List<Movie>();

            List<int> idList = new List<int>();
            // idList.AddRange(1,2,3);
            idList.Add(1);
            idList.Add(2);
            idList.Add(3);
            idList.Add(12);
            foreach (Movie movie in movies)
            {
                if (idList.Contains(movie.Id))
                    topMovies.Add(movie);
            }
            
            return topMovies;
        }
    }
}
