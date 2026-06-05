using System.ComponentModel.DataAnnotations;

namespace CineScope.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [MaxLength(200)]
        public string Title { get; set; }
        [MaxLength(200)]
        public string? SubTitle { get; set; }
        [MaxLength(200)]
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        [MaxLength(20)]
        public string Rating { get; set; }
        [MaxLength(100)]
        public string Duration { get; set; }
        [MaxLength(2000)]
        public string Description { get; set; }
        [MaxLength(100)]
        public string? PosterFilename { get; set; }
        [MaxLength(100)]
        public string? PosterFilenameWide { get; set; }
        [MaxLength(50)]
        public string FromCountry { get; set; }
        [MaxLength(100)]
        public string? Director { get; set; }

        public List<Actor>? Actors;

        public List<int>? ActorIds;

        [MaxLength(50)]
        public string? Language { get; set; }        
        public string? Budget { get; set; }
       
        public Movie() {}
    }
}
