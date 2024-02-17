using System.ComponentModel.DataAnnotations;

namespace CafeActivity.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string UserEmail { get; set; }

        [Required]
        public string UserPassword { get; set; }
    }
}
