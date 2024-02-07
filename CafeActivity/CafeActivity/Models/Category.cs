using System.ComponentModel.DataAnnotations;

namespace CafeActivity.Models
{
    public class Category
    {
        public Category()
        {
            Activities = new HashSet<Activity>();
        }
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public string? CategoryImageUrl { get; set; }
        public bool CategoryStatus { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
    }
}
