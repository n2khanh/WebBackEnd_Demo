using System.ComponentModel.DataAnnotations;

namespace Zoo_template.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int? Age { get; set; }
        [Required]
        public int? Gender { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
