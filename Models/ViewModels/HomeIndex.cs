using Microsoft.AspNetCore.Mvc.Rendering;

namespace CineScope.Models.ViewModels
{
    public class HomeIndex
    {
        public Movie movie { get; set; }
        public List<Movie> movies { get; set; }

        public List<Movie>? topMovies { get; set; }        
        
        public List<SelectListItem> genrerListItems { get; set; }

        public string genre { get; set; }

        public string specialString { get; set; }

        public int index { get; set; }

        public List<Actor> actors { get; set; }

        public HomeIndex(List<Movie> movies, List<SelectListItem> genrerListItems)
        {
            this.movies = movies;
            this.genrerListItems = genrerListItems;
        }

        public HomeIndex(List<Movie> movies, List<Movie> topMovies,  List<SelectListItem> genrerListItems)
        {
            this.movies = movies;
            this.genrerListItems = genrerListItems;
            this.topMovies = topMovies;
        }

        public HomeIndex(Movie movie, List<Actor> actors)
        {
            this.movie = movie;
            this.actors = actors;
        }
    }
}
