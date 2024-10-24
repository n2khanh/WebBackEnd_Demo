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
    public int CageId { get; set; }

    [Column("AreaID")]
    public int? AreaId { get; set; }

    public int? Quantity { get; set; }

    public int? MaxQuantity { get; set; }

    [StringLength(255)]
    public string? Note { get; set; }

    [ForeignKey("AreaId")]
    [InverseProperty("TCages")]
    public virtual TArea? Area { get; set; }

    [InverseProperty("Cage")]
    public virtual ICollection<TAnimal> TAnimals { get; set; } = new List<TAnimal>();
}
