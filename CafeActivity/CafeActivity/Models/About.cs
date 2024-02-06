using System.ComponentModel.DataAnnotations;

namespace CafeActivity.Models
{
    public class About
    {
        [Key]
        public int Id { get; set; }
        public string AboutDetails { get; set; }
        public string? AboutImageUrl { get; set; }
        public string AboutMapLocation { get; }
        public bool AboutStatus { get; set; }
    }
}
