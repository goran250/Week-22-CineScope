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

            // Denna metoden lägger till data i en tom databas. Den ska bara köras bara en gång när databasen är tom.
            // AddData();
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
                                        "PosterFilenameWide,FromCountry,Director,Actors,ActorIds,Language,Budget")] Movie movie, List<int> actorIds)
        {
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
                           "PosterFilename,PosterFilenameWide,FromCountry,Director,Actors,ActorIds,Language,Budget")] Movie movie, List<int> actorIds)
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

        private void AddData()
        {
            if (!cineScopeDbContext.Movies.Any())
            {
                Movie movie = new Movie();
                movie.Title = "Sällskapsresan";
                movie.SubTitle = "eller finns det svenskt kaffe på grisfesten?";
                movie.Genre = "Komedi";
                movie.ReleaseDate = new DateTime(1980, 8, 22);
                movie.Rating = "8.2";
                movie.Duration = "1h 47min";
                movie.Description = "Sällskapsresan, egentligen Sällskapsresan eller finns det svenskt kaffe på grisfesten, är en svensk komedifilm" +
                                    "från 1980 i regi av Lasse Åberg. I huvudrollerna ses Lasse Åberg och Jon Skolmen. Det är den första filmen i en" +
                                    "serie om och med antihjälten Stig-Helmer Olsson.";
                movie.PosterFilename = "sallskapsresan.webp";
                movie.PosterFilenameWide = "sallskapsresan_wide.webp";
                movie.FromCountry = "Sverige";
                movie.Director = "Lasse Åberg, Peter Hald";
                movie.Language = "Svenska";
                movie.Budget = "3,4 miljoner";

                cineScopeDbContext.Movies.Add(movie);

                movie = new Movie();
                movie.Title = "Snowroller";
                movie.SubTitle = "Sällskapsresan 2";
                movie.Genre = "Komedi";
                movie.ReleaseDate = new DateTime(1985, 10, 4);
                movie.Rating = "9.2";
                movie.Duration = "1h 31min";
                movie.Description = "Snowroller - Sällskapsresan 2 är en svenskkomedifilm från 1985 i regi av Lasse Åberg och Peter Hald. I huvudrollerna ses Lasse Åberg och Jon Skolmen. Stig-Helmer Olsson reser tillsammans med Ole Bramserud till schweiziska Alperna för skidsemester, i orten Kirchberg. På resan får de sällskap med ett antal andra personligheter, bland annat familjen Jönsson, den pratglade fabrikören Brännström och dennes tystlåtne kamrer Hedlund från den norrländska landsbygden.;";
                movie.PosterFilename = "snowroller.webp";
                movie.PosterFilenameWide = "snowroller_wide.webp";
                movie.FromCountry = "Sverige";
                movie.Director = "Lasse Åberg, Peter Hald";
                movie.Language = "Svenska";
                movie.Budget = "Ej känt";

                cineScopeDbContext.Movies.Add(movie);

                movie = new Movie();
                movie.Title = "Jurassic Park";
                movie.SubTitle = "";
                movie.Genre = "Science fiction, Äventyr";
                movie.ReleaseDate = new DateTime(1993, 5, 11);
                movie.Rating = "9.0";
                movie.Duration = "2h 2min";
                movie.Description = "Jurassic Park är en amerikansk science fiction-äventyrsfilm från 1993, regisserad av Steven Spielberg med manus skrivet av Michael Crichton och David Koepp, baserad på Crichtons roman Urtidsparken från 1990. Filmen, med Sam Neill, Laura Dern, Jeff Goldblum och Richard Attenborough i huvudrollerna, utspelar sig på den fiktiva ön Isla Nublar nära Costa Rica, där den rike affärsmannen John Hammond (Attenborough) och ett team av genetiska forskare har skapat en djurpark med utdöda dinosaurier. När industriellt sabotage leder till en katastrofal avstängning av parkens elektriska stängsel och säkerhetssystem, kämpar en liten grupp besökare för att överleva och fly från den nu farliga ön.";
                movie.PosterFilename = "jurassic_park.webp";
                movie.PosterFilenameWide = "jurassic_park_wide.webp";
                movie.FromCountry = "Usa";
                movie.Director = "Steven Spielberg";
                movie.Language = "Engelska";
                movie.Budget = "163 miljoner dollar";

                cineScopeDbContext.Movies.Add(movie);

                movie = new Movie();
                movie.Title = "The lost world";
                movie.SubTitle = "Jurassic Park";
                movie.Genre = "Science fiction, Äventyr";
                movie.ReleaseDate = new DateTime(1993, 5, 11);
                movie.Rating = "9.0";
                movie.Duration = "2h";
                movie.Description = "The Lost World är en amerikansk oscarnominerad film som hade biopremiär i USA den 23 maj 1997, i regi av Steven Spielberg efter Michael Crichtons roman En försvunnen värld.  Öarna Isla Nublar och Isla Sorna har, efter förödelsen i djurparken \"Jurassic Park\", lämnats åt sina öden. Parkarbetarna har flytt och den lokala industrin har avvecklats. Det genetikföretag som stod bakom projektet, InGen, står nu inför en finansiell kris. För att undvika konkurs så beslutas det att ön Isla Sorna, där ursprungligen dinosaurierna framavlades, skall exploateras. I stället för att återuppbygga denna enorma attraktion på dessa avlägsna öar företaget har hyrt utanför Costa Ricas kust, så har man tagit det kontroversiella beslutet att flytta djuren till fastlandet i stället, mer bestämt till San Diego, USA. Företagets före detta styrelseordförande John Hammond (Sir Richard Attenborough) har mist kontrollen över företaget och för att värna om de djur han en gång stod som ansvarig för, så skickar han i hemlighet en mindre dokumentärstyrka till ön i ett försök att hinna före InGen:s officiella insatsstyrka. Dessa skall, med kamera och video, dokumentera djuren \"i dess naturliga miljö\" i ett sista desperat hopp att vända världsopinionen så att isoleringen av De Fem Dödens Öar blir lagskyddade och i fortsättningen förbli obesökta. Men saker går inte som planerat och John Hammonds expedition och InGens expedition blir strandsatta på ön. De båda grupperna måste slå sig samman för att komma hem igen. Men snart inser de att denna förlorade värld hyser hungriga jägare i mörkret och bara de mest utvecklade människorna i gruppen överlever utmaningen ...";
                movie.PosterFilename = "the-lost-world-jurassic_park.webp";
                movie.PosterFilenameWide = "the-lost-world-jurassic-park-wide.webp";
                movie.FromCountry = "Usa";
                movie.Director = "Steven Spielberg";
                movie.Language = "Engelska";
                movie.Budget = "73 miljoner dollar";

                cineScopeDbContext.Movies.Add(movie);

                movie = new Movie();
                movie.Title = "Interstellar";
                movie.SubTitle = "Jurassic Park";
                movie.Genre = "Science fiction";
                movie.ReleaseDate = new DateTime(2014, 11, 14);
                movie.Rating = "7.5";
                movie.Duration = "2h 49min";
                movie.Description = "I en nära framtid när människan inte längre kan leva på jordens tillgångar får en grupp forskare och upptäckare det viktigaste uppdraget i mänsklighetens historia: att resa bortom via ett nyupptäckt s.k. maskhål till en annan galax för att ta reda på om mänskligheten genom att flytta dit kan överleva.";
                movie.PosterFilename = "interstellar.webp";
                movie.PosterFilenameWide = "interstellar-bred.webp";
                movie.FromCountry = "Usa";
                movie.Director = "Christopher Nolan";
                movie.Language = "Engelska";
                movie.Budget = "165 miljoner dollar";

                cineScopeDbContext.Movies.Add(movie);

                movie = new Movie();
                movie.Title = "Den ofrivillige golfaren";
                movie.SubTitle = "Sällskapresan 4";
                movie.Genre = "Komedi";
                movie.ReleaseDate = new DateTime(1991, 12, 25);
                movie.Rating = "9";
                movie.Duration = "1h 43min";
                movie.Description = "Den ofrivillige golfaren hade biopremiär i Sverige den 25 december 1991.  Med Lasse Åberg som regissör och med Lasse Åberg och Jon Skolmen i huvudrollerna är detta den fjärde filmen i serien om Stig-Helmer Olsson.\r\n\r\nDet är högkonjunktur i Sverige och Stig-Helmer börjar jobba som gatsopare efter att brödrostfabriken Toastmaster lagts ner av finansbolaget Parvus Finans av rationaliseringsskäl. Under sin första arbetsdag, då han plockar skräp vid en golfbana, får han ett erbjudande; storfinansmannen Bruno Anderhage och galleristen Mette har slagit vad om att vem som helst kan lära sig spela golf lika bra om inte bättre än Bruno på en vecka, och \"vem som helst\" blir tafatte Stig-Helmer.\r\n\r\nStig-Helmer letar upp morbror Julles gamla golfutrustning från 1920-talet och börjar träna och får hjälp av Ole men det visar sig att det behövs ett smärre mirakel och de beslutar sig för att åka till Skottland och be om hjälp av golflegendaren Roderic McDougall.";
                movie.PosterFilename = "den-ofrivillige-golfaren.webp";
                movie.PosterFilenameWide = "den-ofrivillige-golfaren-bred.webp";
                movie.FromCountry = "Sverige";
                movie.Director = "Lasse Åberg";
                movie.Language = "Svenska";
                movie.Budget = "15-16 miljoner kr";

                cineScopeDbContext.Movies.Add(movie);

                movie = new Movie();
                movie.Title = "Varning för Jönsonligan";
                movie.SubTitle = "";
                movie.Genre = "Komedi";
                movie.ReleaseDate = new DateTime(1981, 12, 04);
                movie.Rating = "8,5";
                movie.Duration = "1h 31min";
                movie.Description = "Jönssonligan är en svensk komedifilmserie om en kriminell trio med samma namn.\r\n Varning för Jönssonligan, hade biopremiär den 4 december 1981 med Charles-Ingvar \"Sickan\" Jönsson (Gösta Ekman), Ragnar Vanheden (Ulf Brunnberg) och den finlandssvenske Rocky (Nils Brandt) i huvudrollerna.";
                movie.PosterFilename = "varning-for-jonsonligan.webp";
                movie.PosterFilenameWide = "varning-for-jonsonligan-wide.webp";
                movie.FromCountry = "Sverige";
                movie.Director = "Jonas Cornell";
                movie.Language = "Svenska";
                movie.Budget = "4 miljoner kr";

                cineScopeDbContext.Movies.Add(movie);

                movie = new Movie();
                movie.Title = "Avatar";
                movie.SubTitle = "";
                movie.Genre = "Science fiction";
                movie.ReleaseDate = new DateTime(2009, 12, 18);
                movie.Rating = "7,5";
                movie.Duration = "2h 42min";
                movie.Description = "Avatar är en amerikansk science fiction-film, regisserad och producerad James Cameron. \r\nÅret är 2154 och människan har tvingats kolonisera och exploatera en ny planet (måne) för att inte gå under på den gamla. Med hjälp av gigantiska ”hell trucks”, förvillande lika de väldiga maskiner som i dagbrott bryter brunkol i Tyskland och de jättelastbilar som fraktar oljesand i Kanada, utvinns grundämnet unobtainium – en nödvändig komponent i jordens energiförsörjning. Ursprungsbefolkningen mördas och skogen skövlas.\r\n\r\nFilmens protagonist, Jake Sully (Sam Worthington), är en före detta marinkårssoldat, som skadats i strider på jorden och blivit förlamad från midjan. Han får chansen att delta i Avatar-programmet, genom vilket han ges möjligheten att åter kunna gå normalt.\r\n\r\nGenom Avatar-programmet reser Jake till Pandora, en himlakropp täckt av grönskande djungel, fylld av fantastiska livsformer. Pandora är även hem för Na’vi, en humanoid ras som anses primitiv, men som är fysiskt kraftfullare än människor. Na’vi har svans, blå hud, är tre meter långa och lever i harmoni med sin oförstörda värld. Då människorna tränger djupare in i Pandoras skogar på jakt efter värdefulla mineraler, tvingas Na’vi att strida för sin överlevnad.\r\nJake har ovetandes blivit rekryterad för att fortsätta med övergreppen. Eftersom människor inte kan andas atmosfären på Pandora, har människan genetiskt skapat Avatarerna, vilka är hybrider av människor och Na’vi. Genom sin Avatarkropp återfår Jake full rörelsefrihet och han sänds in i Pandoras djungel som spanare för de soldater som skall komma efter honom. Där upplever han Pandoras skönhet och faror och möter en ung Na’vi-kvinna, Neytiri (Zoe Saldaña) som han förälskar sig i.\r\n\r\nPå grund av det finner han sig vara fångad mellan det militär-industriella styrkorna från jorden och Na’vi. Han tvingas välja sida i en strid, som kommer att avgöra Pandoras öde.";
                movie.PosterFilename = "avatar-1.webp";
                movie.PosterFilenameWide = "avatar-1-wide.webp";
                movie.FromCountry = "Usa";
                movie.Director = "James Cameron";
                movie.Language = "Engelska";
                movie.Budget = "237 miljoner dollar";

                cineScopeDbContext.Movies.Add(movie);

                movie = new Movie();
                movie.Title = "Hälsoresan";
                movie.SubTitle = "En smal film av stor vikt";
                movie.Genre = "Komedi";
                movie.ReleaseDate = new DateTime(1999, 12, 25);
                movie.Rating = "9";
                movie.Duration = "1h 45min";
                movie.Description = "Hälsoresan – En smal film av stor vikt är en svensk komedifilm som hade biopremiär i Sverige den 25 december 1999, i regi av Lasse Åberg med Lasse Åberg och Jon Skolmen i huvudrollerna. Filmen är den femte filmen i serien om Stig Helmer Olsson.";
                movie.PosterFilename = "halsoresan.webp";
                movie.PosterFilenameWide = "halsoresan-bred.webp";
                movie.FromCountry = "Sverige";
                movie.Director = "Lasse Åberg";
                movie.Language = "Svenska";
                movie.Budget = "15-16 miljoner kr";

                cineScopeDbContext.Movies.Add(movie);

                cineScopeDbContext.SaveChanges();
            }

            if (!cineScopeDbContext.Actors.Any())
            {
                Actor actor = new Actor();
                actor.Name = "Lasse Åberg";
                actor.Country = "Sverige";
                actor.PictureFilename = "lasse-berg.webp";

                cineScopeDbContext.Actors.Add(actor);

                actor = new Actor();
                actor.Name = "Jon Skolmen";
                actor.Country = "Sverige";
                actor.PictureFilename = "jon-skolmen.webp";

                cineScopeDbContext.Actors.Add(actor);

                actor = new Actor();
                actor.Name = "Kim Anderzon";
                actor.Country = "Sverige";
                actor.PictureFilename = "kim-anderzon.webp";

                cineScopeDbContext.Actors.Add(actor);

                actor = new Actor();
                actor.Name = "Lottie Ejebrant";
                actor.Country = "Sverige";
                actor.PictureFilename = "lottie-ejebrant.webp";

                cineScopeDbContext.Actors.Add(actor);

                actor = new Actor();
                actor.Name = "Sven Melander";
                actor.Country = "Sverige";
                actor.PictureFilename = "sven-melander.webp";

                cineScopeDbContext.Actors.Add(actor);

                actor = new Actor();
                actor.Name = "Weiron Holmberg";
                actor.Country = "Sverige";
                actor.PictureFilename = "weiron-holmberg.webp";

                cineScopeDbContext.Actors.Add(actor);

                actor = new Actor();
                actor.Name = "Magnus Härenstam";
                actor.Country = "Sverige";
                actor.PictureFilename = "magnus-harenstam.webp";

                cineScopeDbContext.Actors.Add(actor);

                actor = new Actor();
                actor.Name = "Roland Jansson";
                actor.Country = "Sverige";
                actor.PictureFilename = "roland-jansson.webp";

                cineScopeDbContext.Actors.Add(actor);

                actor = new Actor();
                actor.Name = "Cecilia Wallton";
                actor.Country = "Sverige";
                actor.PictureFilename = "cecilia-wallton.webp";

                cineScopeDbContext.Actors.Add(actor);

                actor = new Actor();
                actor.Name = "Eva Millberg";
                actor.Country = "Sverige";
                actor.PictureFilename = "eva-millberg.webp";

                cineScopeDbContext.Actors.Add(actor);

                actor = new Actor();
                actor.Name = "Bengt Andersson";
                actor.Country = "Sverige";
                actor.PictureFilename = "bengt-andersson.webp";

                cineScopeDbContext.Actors.Add(actor);

                actor = new Actor();
                actor.Name = "Sam Neill";
                actor.Country = "Usa";
                actor.PictureFilename = "sam-neill.webp";

                cineScopeDbContext.Actors.Add(actor);

                actor = new Actor();
                actor.Name = "Richard Attenborough";
                actor.Country = "Storbritannien";
                actor.PictureFilename = "richard-attenborough.webp";

                cineScopeDbContext.Actors.Add(actor);

                actor = new Actor();
                actor.Name = "Laura Dern";
                actor.Country = "Usa";
                actor.PictureFilename = "laura-dern.webp";

                cineScopeDbContext.Actors.Add(actor);

                actor = new Actor();
                actor.Name = "Jessica Chastain";
                actor.Country = "Usa";
                actor.PictureFilename = "jessica-chastain.webp";

                cineScopeDbContext.Actors.Add(actor);

                actor = new Actor();
                actor.Name = "Matthew McConaughey";
                actor.Country = "Usa";
                actor.PictureFilename = "matthew-mcconaughey.webp";

                cineScopeDbContext.Actors.Add(actor);

                actor = new Actor();
                actor.Name = "Jeff Goldblum";
                actor.Country = "Usa";
                actor.PictureFilename = "jeff-goldblum.webp";

                cineScopeDbContext.Actors.Add(actor);

                actor = new Actor();
                actor.Name = "Sam Worthington";
                actor.Country = "Usa";
                actor.PictureFilename = "sam-worthington.webp";

                cineScopeDbContext.Actors.Add(actor);

                actor = new Actor();
                actor.Name = "Zoe Saldana";
                actor.Country = "Usa";
                actor.PictureFilename = "zoe-saldana.webp";

                cineScopeDbContext.Actors.Add(actor);

                actor = new Actor();
                actor.Name = "Michelle Rodriguez";
                actor.Country = "Usa";
                actor.PictureFilename = "michelle-rodriguez.webp";

                cineScopeDbContext.Actors.Add(actor);

                actor = new Actor();
                actor.Name = "Stephen Lang";
                actor.Country = "Usa";
                actor.PictureFilename = "stephen-lang.webp";

                cineScopeDbContext.Actors.Add(actor);

                actor = new Actor();
                actor.Name = "Julianne Moore";
                actor.Country = "Usa";
                actor.PictureFilename = "julianne-moore.webp";

                cineScopeDbContext.Actors.Add(actor);

                actor = new Actor();
                actor.Name = "Pete Postlethwaite";
                actor.Country = "Usa";
                actor.PictureFilename = "pete-postlethwaite.webp";

                cineScopeDbContext.Actors.Add(actor);

                actor = new Actor();
                actor.Name = "Matt Damon";
                actor.Country = "Usa";
                actor.PictureFilename = "matt-damon.webp";

                cineScopeDbContext.Actors.Add(actor);

                actor = new Actor();
                actor.Name = "Anne Hathaway";
                actor.Country = "Storbritannien";
                actor.PictureFilename = "anne-hathaway.webp";

                cineScopeDbContext.Actors.Add(actor);

                actor = new Actor();
                actor.Name = "Michael Caine";
                actor.Country = "Usa";
                actor.PictureFilename = "michael-caine.webp";

                cineScopeDbContext.Actors.Add(actor);

                actor = new Actor();
                actor.Name = "Mats Bergman";
                actor.Country = "Sverige";
                actor.PictureFilename = "mats-bergman.webp";

                cineScopeDbContext.Actors.Add(actor);

                actor = new Actor();
                actor.Name = "Jimmy Logan";
                actor.Country = "Storbritannien";
                actor.PictureFilename = "jimmy-logan.webp";

                cineScopeDbContext.Actors.Add(actor);

                cineScopeDbContext.SaveChanges();
            }

            if (!cineScopeDbContext.ActorsMovies.Any())
            {

            }
        }
    }
}
