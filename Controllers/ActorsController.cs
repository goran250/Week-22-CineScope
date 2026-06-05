
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CineScope.Models;

namespace CineScope.Controllers
{
    public class ActorsController : Controller
    {
        private readonly CineScopeDbContext appDbContext;

        public ActorsController(CineScopeDbContext context)
        {
            appDbContext = context;
        }

        // GET: ACTORS
        public async Task<IActionResult> Index()
        {
            return View(await appDbContext.Actors.ToListAsync());
        }

        // GET: ACTORS/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await appDbContext.Actors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        // GET: ACTORS/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ACTORS/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Country,PicturFilename,Movies")] Actor actor)
        {
            if (ModelState.IsValid)
            {
                appDbContext.Add(actor);
                await appDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(actor);
        }

        // GET: ACTORS/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await appDbContext.Actors.FindAsync(id);
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
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Name,Country,Movies")] Actor actor)
        {
            if (id != actor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    appDbContext.Update(actor);
                    await appDbContext.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            return View(actor);
        }

        // GET: ACTORS/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await appDbContext.Actors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        // POST: ACTORS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var actor = await appDbContext.Actors.FindAsync(id);
            if (actor != null)
            {
                appDbContext.Actors.Remove(actor);
            }

            await appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActorExists(int? id)
        {
            return appDbContext.Actors.Any(e => e.Id == id);
        }
    }
}
