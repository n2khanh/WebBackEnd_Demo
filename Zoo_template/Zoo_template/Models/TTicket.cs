using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Zoo_template.Models;

[Table("tTicket")]
public partial class TTicket
{
    [Key]
    [Column("TicketID")]
    public int TicketId { get; set; }

    [StringLength(100)]
    public string? TicketName { get; set; }

    public TimeOnly? Time { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Price { get; set; }

    [InverseProperty("Ticket")]
    public virtual ICollection<TGuest> TGuests { get; set; } = new List<TGuest>();
}
