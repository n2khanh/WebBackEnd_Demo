using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Zoo_template.Models;

[Table("tShift")]
public partial class TShift
{
    [Key]
    [Column("ShiftID")]
    public int ShiftId { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    public TimeOnly? TimBegin { get; set; }

    public TimeOnly? TimeFinish { get; set; }

    [InverseProperty("Shift")]
    public virtual ICollection<TEmployee> TEmployees { get; set; } = new List<TEmployee>();
}
