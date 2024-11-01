using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Zoo_template.Models;

[Table("tTicket")]
public partial class TTicket
{
    [Key]
    [Column("TicketID")]
    [Required(ErrorMessage = "Vui lòng nhập loại vé ")]
    [Display(Name = "Loại vé")]
    public int TicketId { get; set; }

    [StringLength(100)]
    [Required(ErrorMessage = "Vui lòng nhập tên vé ")]
    [Display(Name = "Vé")]
    public string? TicketName { get; set; }
    [Display(Name = "Thời gian mở cửa")]
    public TimeOnly? Time { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    
    [Display(Name = "Giá vé")]
    public decimal? Price { get; set; }

    [InverseProperty("Ticket")]
    public virtual ICollection<TGuest> TGuests { get; set; } = new List<TGuest>();
}
