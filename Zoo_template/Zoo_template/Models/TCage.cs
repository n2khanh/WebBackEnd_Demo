using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Zoo_template.Models;

[Table("tCage")]
public partial class TCage
{
    [Key]
    [Column("CageID")]
    [Display(Name ="Mã chuồng")]
    public int CageId { get; set; }

    [Column("AreaID")]

    [Display(Name = "Khu vực")]
    [Required(ErrorMessage = "Vui lòng cho biết khu vực")]
    public int? AreaId { get; set; }
    [Display(Name = "Số lượng")]
    public int? Quantity { get; set; }

    [Display(Name = "Số lượng tối đa")]
    [Required(ErrorMessage ="Vui lòng cho biết số lượng tối đa")]
    public int? MaxQuantity { get; set; }

    [StringLength(255)]
    [Display(Name = "Ghi chú")]
    public string? Note { get; set; }

    [ForeignKey("AreaId")]
    [InverseProperty("TCages")]
    [Display(Name = "Khu vực")]
    public virtual TArea? Area { get; set; }

    [InverseProperty("Cage")]
    public virtual ICollection<TAnimal> TAnimals { get; set; } = new List<TAnimal>();
}
