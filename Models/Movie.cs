using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [MaxLength(50)]
        public string? Language { get; set; }
        public string? Budget { get; set; }
        
        [NotMapped] // Används bara på startsidan för numrera frames i bildspelet
        public string? FrameNbr { get; set; }
        [NotMapped] // Används bara på startsidan för att sätta css-klasser på frames i bildspelet
        public string? FrameClasses { get; set; }
        /**
        [NotMapped]
        public List<Actor>? Actors;        
        [NotMapped]
        public List<int>? ActorIds;
        */

        public Movie() {}
    }
}
