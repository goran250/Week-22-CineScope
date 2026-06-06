using CineScope.Models;
using CineScope.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;

namespace CineScope.Controllers
{
    public class AdminController : Controller
    {
        private readonly CineScopeDbContext cineScopeDbContext;

        public AdminController(CineScopeDbContext context)
        {
            cineScopeDbContext = context;
        }

        // GET: MOVIES
        public async Task<IActionResult> Index(string? searchValue, string? genre)
        {
            List<Movie> movies;
            List<SelectListItem> genrerListItems = new List<SelectListItem>();

            movies = await cineScopeDbContext.Movies.ToListAsync();

            genrerListItems.Add(new SelectListItem { Value = "Alla genrer", Text = "Alla genrer" });

            foreach (Movie movie in movies)
            {
                // Ser till så att alla genrer är unika.
                if (!genrerListItems.Any(g => g.Value == movie.Genre))
                {
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

            AdminCrud adminCrud = new AdminCrud(movies, genrerListItems);

            return View(adminCrud);
        }



        // GET: MOVIES/Details/5
        public async Task<IActionResult> MovieDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await cineScopeDbContext.Movies.FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            List<int> actorIds = await cineScopeDbContext.ActorsMovies.Where(am => am.MoviesId == movie.Id).Select(am => am.ActorsId).ToListAsync();
            List<Actor> actors = await cineScopeDbContext.Actors.Where(a => actorIds.Contains(a.Id)).ToListAsync();

            AdminCrud adminCrud = new AdminCrud(movie, actors);

            return View(adminCrud);
        }

        // GET: MOVIES/Create
        public IActionResult CreateMovie()
        {
            List<Actor> actors = cineScopeDbContext.Actors.ToList();

            List<SelectListItem> actorListItems = new List<SelectListItem>();

            foreach (Actor actor in actors)
            {
                actorListItems.Add(new SelectListItem(actor.Name, actor.Id.ToString()));
            }

            AdminCrud adminCrud = new AdminCrud(new Movie(), actorListItems, "actor");
            return View(adminCrud);
        }

        // POST: MOVIES/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMovie([Bind("Id,Title,SubTitle,Genre,ReleaseDate,Rating,Duration,Description,PosterFilename," +
                           "PosterFilenameWide,FromCountry,Director,Actors,Language,Budget")] Movie movie, List<int> actorIds)
                           // "PosterFilenameWide,FromCountry,Director,Actors,Language,Budget")] Movie movie, List<int> actorIds, IFormFile posterImage)
        {
            /** Kod för filuppladdning, men den fungerar inte.
            if (posterImage != null && posterImage.Length > 0)
            {
                string filePath = "~/images/" + posterImage.FileName;

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await posterImage.CopyToAsync(stream);
                }

                movie.PosterFilename = posterImage.FileName;
            }
            */

            if (ModelState.IsValid)
            {

                cineScopeDbContext.Add(movie);
                await cineScopeDbContext.SaveChangesAsync();

                foreach (int actorId in actorIds)
                {
                    ActorMovie actorMovie = new ActorMovie(movie.Id, actorId);
                    cineScopeDbContext.Add(actorMovie);
                }

                await cineScopeDbContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: MOVIES/Edit/5
        public async Task<IActionResult> EditMovie(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await cineScopeDbContext.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            List<int> actorIds = await cineScopeDbContext.ActorsMovies.Where(am => am.MoviesId == movie.Id).Select(am => am.ActorsId).ToListAsync();

            List<Actor> actors = cineScopeDbContext.Actors.ToList();

            List<SelectListItem> actorListItems = new List<SelectListItem>();

            foreach (Actor actor in actors)
            {
                if (actorIds.Contains(actor.Id))
                    actorListItems.Add(new SelectListItem(actor.Name, actor.Id.ToString(), selected: true));
                else
                    actorListItems.Add(new SelectListItem(actor.Name, actor.Id.ToString()));
            }

            AdminCrud adminCrud = new AdminCrud(movie, actorListItems, "actor");

            return View(adminCrud);
        }


        // POST: MOVIES/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMovie(int? id, [Bind("Id,Title,SubTitle,Genre,ReleaseDate,Rating,Duration,Description," +
                           "PosterFilename,PosterFilenameWide,FromCountry,Director,Actors,Language,Budget")] Movie movie, List<int> actorIds)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    cineScopeDbContext.Update(movie);

                    // Tar bort alla kopplingar mellan filmen och skådespelarna i ActorsMovies-tabellen för att sedan lägga till de nya kopplingarna.
                    cineScopeDbContext.ActorsMovies.RemoveRange(cineScopeDbContext.ActorsMovies.Where(am => am.MoviesId == movie.Id).ToList());

                    await cineScopeDbContext.SaveChangesAsync();

                    foreach (int actorId in actorIds)
                    {
                        ActorMovie actorMovie = new ActorMovie(movie.Id, actorId);
                        cineScopeDbContext.Add(actorMovie); // Lägger till de nya kopplingarna i ActorsMovies-tabellen.
                    }

                    await cineScopeDbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: MOVIES/Delete/5
        public async Task<IActionResult> DeleteMovie(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await cineScopeDbContext.Movies.FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: MOVIES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var movie = await cineScopeDbContext.Movies.FindAsync(id);
            if (movie != null)
            {
                cineScopeDbContext.Movies.Remove(movie);
            }

            await cineScopeDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int? id)
        {
            return cineScopeDbContext.Movies.Any(e => e.Id == id);
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


        // GET: ACTORS/Create
        public IActionResult CreateActor()
        {
            return View();
        }

        // POST: ACTORS/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateActor([Bind("Id,Name,Country,PictureFilename,Movies")] Actor actor)
        {
            if (ModelState.IsValid)
            {
                cineScopeDbContext.Add(actor);
                await cineScopeDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(AllActors));
            }
            return View(actor);
        }

        // GET: ACTORS/Edit/5
        public async Task<IActionResult> EditActor(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await cineScopeDbContext.Actors.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }
            return View(actor);
        }

        // POST: ACTORS/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditActor(int? id, [Bind("Id,Name,Country,PictureFilename,Movies")] Actor actor)
        {
            if (id != actor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    cineScopeDbContext.Update(actor);
                    await cineScopeDbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActorExists(actor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(AllActors));
            }
            return View();
        }

        // GET: ACTORS/Delete/5
        public async Task<IActionResult> DeleteActor(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await cineScopeDbContext.Actors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        // POST: ACTORS/Delete/5
        [HttpPost, ActionName("DeleteActor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteActorConfirmed(int? id)
        {
            var actor = await cineScopeDbContext.Actors.FindAsync(id);
            if (actor != null)
            {
                cineScopeDbContext.Actors.Remove(actor);
            }

            await cineScopeDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(AllActors));
        }

        private bool ActorExists(int? id)
        {
            return cineScopeDbContext.Actors.Any(e => e.Id == id);
        }
    }
}
