using System.ComponentModel.DataAnnotations;

namespace RatedIn.ViewModels
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}