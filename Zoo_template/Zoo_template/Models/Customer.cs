using Humanizer;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Zoo_template.Models
{
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime dateValue)
            {
                if (dateValue < DateTime.Now.Date)
                {
                    return new ValidationResult("Ngày không hợp lệ. Vui lòng chọn ngày trong tương lai.");
                }
            }
            return ValidationResult.Success;
        }
    }

    public partial class Customer
    {
        [Required(ErrorMessage = "Họ tên không được để trống")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Tên phải có từ 4 đến 100 ký tự")]
        [Display(Name = "Họ tên")]
        public string? Fullname {  get; set; }
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Số điện thoại phải bao gồm đúng 10 chữ số.")]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email không được để trống")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Địa chỉ email phải có đuôi @gmail.com")]
        public string? Email {  get; set; }
        [Required(ErrorMessage = "Số lượng vé không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng vé phải lớn hơn 0")]
        public int? numberofTicket {  get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{8,}$",
        ErrorMessage = "Mật khẩu phải có ít nhất 8 ký tự, bao gồm chữ hoa, chữ thường, số và ký tự đặc biệt")]
        public string? Password {  get; set; }
        [Required]
        [FutureDate(ErrorMessage = "Ngày không hợp lệ. Vui lòng chọn ngày trong tương lai.")]
        [DataType(DataType.Date)]
        public DateTime? Date {  get; set; }
        [Required]
        public Gender? Gender { get; set; }
        [Required]
        public PayMethod? PayMethod { get; set; }
        [Required]
        public TypeTicket? typeofTicket { get; set; }

    }
}
