
using System.ComponentModel.DataAnnotations;
namespace Zoo_template.Models
{
    public class TLogin
    {
        [Required(ErrorMessage ="Vui lòng nhập tên người dùng")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập khẩu")]
        [StringLength(8,ErrorMessage ="Mậ khẩu phải có ít nhất 8 kí tự")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tuổi")]
        public int? Age { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập giới tính")]
        public int? Gender { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập email")]
        public string Email { get; set; }
    }
}
