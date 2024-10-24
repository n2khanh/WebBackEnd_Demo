using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Zoo_template.Models;

[Table("tArea")]
public partial class TArea
{
    [Key]
    [Column("AreaID")]
    public int AreaId { get; set; }

    [StringLength(100)]
    public string? AreaName { get; set; }

    [StringLength(255)]
    public string? Note { get; set; }

    [InverseProperty("Area")]
    public virtual ICollection<TCage> TCages { get; set; } = new List<TCage>();

    [InverseProperty("ResAreaNavigation")]
    public virtual ICollection<TEmployee> TEmployees { get; set; } = new List<TEmployee>();
}
