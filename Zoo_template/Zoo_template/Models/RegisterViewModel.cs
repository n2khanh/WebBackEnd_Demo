namespace Zoo_template.Models
{
    public class RegisterViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public int? Gender { get; set; }
        public string Email { get; set; }
    }
}
