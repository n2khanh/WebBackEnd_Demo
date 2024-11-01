using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Zoo_template.Models;

[Table("tGuest")]
public partial class TGuest
{
    [Key]
    [Column("GuestID")]
    public int GuestId { get; set; }

    [StringLength(100)]
    [Required(ErrorMessage = "Vui lòng nhập tên ")]
    [Display(Name = "Tên khách")]
    public string? GuestName { get; set; }
    [Required(ErrorMessage = "Vui lòng nhập ngày sinh ")]
    [Display(Name = "Ngày tháng năm sinh")]
    public DateOnly? DateOfBirth { get; set; }
    [Required(ErrorMessage = "Vui lòng chọn phương thức thanh toán ")]
    [Display(Name = "Phương thức thanh toán")]
    public int? PayMethodID { get; set; }

    [StringLength(20)]
    [Required(ErrorMessage = "Vui lòng nhập số điện thoại ")]
    [Display(Name = "Số điện thoại")]
    public string? PhoneNumber { get; set; }

    [StringLength(255)]
    [Required(ErrorMessage = "Vui lòng nhập địa chỉ ")]
    [Display(Name = "Địa chỉ")]
    public string? Address { get; set; }

    [Column("TicketID")]
    public int? TicketId { get; set; }

    [ForeignKey("PayMethodID")]
    [InverseProperty("TGuests")]
    public virtual TPayMethod? PayMethodNavigation { get; set; }

    [ForeignKey("TicketId")]
    [InverseProperty("TGuests")]
    public virtual TTicket? Ticket { get; set; }
}
