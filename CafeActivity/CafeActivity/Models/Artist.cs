using System.ComponentModel.DataAnnotations;
using static System.Reflection.Metadata.BlobBuilder;

namespace CafeActivity.Models
{
    public class Artist
    {
        public Artist()
        {
            Activities = new HashSet<Activity>();
        }
        [Key]
        public int Id { get; set; }
        public string ArtistName { get; set; }
        public string ArtistDescription { get; set; }
        public DateTime ArtistBirthDate { get; set; }
        public string? ArtistImageUrl { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool ArtistStatus { get; set; }
        public ICollection<Activity> Activities { get; set; }

    }
}
