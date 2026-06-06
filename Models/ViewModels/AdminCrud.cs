using Microsoft.AspNetCore.Mvc.Rendering;

namespace CineScope.Models.ViewModels
{
    public class AdminCrud
    {
        public List<Movie> movies { get; set; }

        public Movie movie { get; set; }

        public List<Actor> actors { get; set; }

        public List<int> actorIds { get; set; }

        public List<SelectListItem> actorListItems { get; set; }
        
        public List<SelectListItem> genrerListItems { get; set; }

        public string genre { get; set; }

        // public IFormFile posterImage { get; set; }

        public AdminCrud(List<Movie> movies, List<SelectListItem> genrerListItems)
        {
            this.movies = movies;
            this.genrerListItems = genrerListItems;
        }

        public AdminCrud(Movie movie, List<SelectListItem> actorListItems, string type)
        {
            this.movie = movie;
            this.actorListItems = actorListItems;
        }

        public AdminCrud(Movie movie, List<Actor> actors)
        {
            this.movie = movie;
            this.actors = actors;
        }

        public AdminCrud(Movie movie, List<Actor> actors, List<int> actorIds)
        {
            this.movie = movie;
            this.actors = actors;
            this.actorIds = actorIds;
        }
    }
}
