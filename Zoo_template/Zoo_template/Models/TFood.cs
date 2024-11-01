using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Zoo_template.Models;

[Table("tFood")]
public partial class TFood
{
    [Key]
    [Column("FoodID")]
    public int FoodId { get; set; }

    [StringLength(100)]
    [Display(Name = "Loại thức ăn ")]
    [Required(ErrorMessage = "Vui lòng cho biết loại thức ăn")]
    public string? FoodName { get; set; }

    [StringLength(100)]
    public string? UseFor { get; set; }

    public int? Quantity { get; set; }

    [InverseProperty("Food")]
    public virtual ICollection<TAnimal> TAnimals { get; set; } = new List<TAnimal>();
}
