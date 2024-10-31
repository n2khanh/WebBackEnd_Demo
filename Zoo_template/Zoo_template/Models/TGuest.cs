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
    public string? GuestName { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public int? PayMethodID { get; set; }

    [StringLength(20)]
    public string? PhoneNumber { get; set; }

    [StringLength(255)]
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
