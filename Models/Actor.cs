using System.ComponentModel.DataAnnotations;

namespace CineScope.Models
{
    public class Actor
    {
        public int Id { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Country { get; set; }
        public List<Movie>? Movies { get; set; }
        [MaxLength(100)]
        public string PictureFilename { get; set; }
    }
}
