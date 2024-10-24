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
    [Required]
    public string? Name { get; set; }

    [StringLength(10)]
    [Required]
    public string? Gender { get; set; }


    [Required(ErrorMessage = "Phone number is required.")]
    [StringLength(10, ErrorMessage = "Phone number cannot be longer than 10 characters.")]
    [RegularExpression(@"^\+?[0-9]{1,10}$", ErrorMessage = "Phone number must be a valid format.")]
    public string? PhoneNumber { get; set; }

    [StringLength(255)]
    [Required]
    public string? Address { get; set; }
    [Required]
    public int? ResArea { get; set; }

    [Column("ShiftID")]
    public int? ShiftId { get; set; }
    [Required]
    public bool? OnWork { get; set; }

    [ForeignKey("ResArea")]
    [InverseProperty("TEmployees")]
    public virtual TArea? ResAreaNavigation { get; set; }

    [ForeignKey("ShiftId")]
    [InverseProperty("TEmployees")]
    public virtual TShift? Shift { get; set; }
}
