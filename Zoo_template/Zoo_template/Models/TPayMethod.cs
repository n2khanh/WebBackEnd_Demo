using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Zoo_template.Models;

[Table("tPayMethod")]
public partial class TPayMethod
{
    [Key]
    [Column("PayMethodID")]
    [Required(ErrorMessage = "Vui lòng nhập phương thức thanh toán ")]
    [Display(Name = "Phương thức thanh toán")]
    public int PayMethodId { get; set; }

    [StringLength(100)]
    public string? MethodName { get; set; }

    public bool? IsPayed { get; set; }

    [InverseProperty("PayMethodNavigation")]
    public virtual ICollection<TGuest> TGuests { get; set; } = new List<TGuest>();
}

