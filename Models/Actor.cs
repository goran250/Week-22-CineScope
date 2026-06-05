using System.ComponentModel.DataAnnotations;

namespace CineScope.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public List<Movie>? Movies { get; set; }
        [MaxLength(100)]
        public string PictureFilename { get; set; }
    }
}
