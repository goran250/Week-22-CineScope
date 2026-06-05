namespace CineScope.Models
{
    public class ActorMovie
    {
        public int Id { get; set; }
        public int MoviesId { get; set; }
        public int ActorsId { get; set; }

        public ActorMovie()
        { }
        public ActorMovie(int moviesId, int actorsId)
        {
            MoviesId = moviesId;
            ActorsId = actorsId;
        }
    }
}
