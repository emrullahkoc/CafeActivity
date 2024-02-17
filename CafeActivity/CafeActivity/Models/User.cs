using System.ComponentModel.DataAnnotations;

namespace CafeActivity.Models
{
    public class User
    {
        public User()
        {
            Tickets = new HashSet<Ticket>();
        }
        [Key]
        public int Id { get; set; }
        public string UserFullName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string? UserImageUrl { get; set; }
        public string Role { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool UserStatus { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
