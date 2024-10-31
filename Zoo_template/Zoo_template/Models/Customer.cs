using Humanizer;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

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
        [Required]
        [FutureDate(ErrorMessage = "Ngày không hợp lệ. Vui lòng chọn ngày trong tương lai.")]
        [DataType(DataType.Date)]
        public DateTime? Date {  get; set; }
        [Required(ErrorMessage = "Ngày sinh không được bỏ trống")]
        [Range(typeof(DateTime), "1/1/1950", "1/1/2024", ErrorMessage = "Ngày sinh không hợp lệ")]
        public DateTime? DateofBirth { get; set; }
        [Required]
       // public PayMethod? PayMethod { get; set; }
        public int? TicketID { get; set; }
        [Required]
        //public virtual TypeTicket? Ticket { get; set; }
       // [Required]
        public string? Address { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public SqlMoney? Price { get; set; }
    }
}
