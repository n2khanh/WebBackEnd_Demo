using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Zoo_template.Models;

[Table("tEmployee")]
public partial class TEmployee
{
    [Key]
    [Column("EmployeeID")]
    public int EmployeeId { get; set; }

    [StringLength(100)]
    public string? Name { get; set; }

    [StringLength(10)]
    public string? Gender { get; set; }

    [StringLength(15)]
    public string? PhoneNumber { get; set; }

    [StringLength(255)]
    public string? Address { get; set; }

    public int? ResArea { get; set; }

    [Column("ShiftID")]
    public int? ShiftId { get; set; }

    public bool? OnWork { get; set; }

    [ForeignKey("ResArea")]
    [InverseProperty("TEmployees")]
    public virtual TArea? ResAreaNavigation { get; set; }

    [ForeignKey("ShiftId")]
    [InverseProperty("TEmployees")]
    public virtual TShift? Shift { get; set; }
}
