using System.ComponentModel.DataAnnotations;

namespace CafeActivity.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        public string ContactFullName { get; set; }
        public string ContactMail { get; set; }
        public string ContactTitle { get; set; }
        public string ContactMessage { get; set; }
        public DateTime ContactDate { get; set; }
        public bool ContactStatus { get; set; }
    }
}
