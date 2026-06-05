using Microsoft.AspNetCore.Mvc.Rendering;

namespace CineScope.Models.ViewModels
{
    public class HomeMovieDetails
    {
        public Movie movie { get; set; }
              
        public List<Actor> actors { get; set; }
      
        public HomeMovieDetails(Movie movie, List<Actor> actors)
        {
            this.movie = movie;
            this.actors = actors;
        }
    }
}
