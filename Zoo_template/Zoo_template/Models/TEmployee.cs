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
    [Required(ErrorMessage ="Vui lòng nhập tên nhân viên")]
    [Display(Name="Tên nhân viên")]
    public string? Name { get; set; }

    [StringLength(10)]
    [Required(ErrorMessage = "Vui lòng nhập giới tính")]
    [Display(Name = "Giới tính")]
    public string? Gender { get; set; }


    [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
    [StringLength(10, ErrorMessage = "Số điện thoại tối thiểu 10 số")]
    [RegularExpression(@"^\+?[0-9]{1,10}$", ErrorMessage = "Số điện thoại phải đúng định dạng")]
    
    [Display(Name = "Số điện thoại")]
    public string? PhoneNumber { get; set; }

    [StringLength(255)]
    [Required]
    [Display(Name = "Địa chỉ")]
    public string? Address { get; set; }
    [Required(ErrorMessage = "Vui lòng chọn khu vực ")]
    [Display(Name = "Tên khu vực")]
    public int? ResArea { get; set; }

    [Column("ShiftID")]
    [Required(ErrorMessage = "Vui lòng chọn ca ")]
    [Display(Name = "Ca")]
    public int? ShiftId { get; set; }
    [Required(ErrorMessage ="Vui lòng chọn trạng thái làm việc")]
    [Display(Name = "Trạng thái")]
    public bool? OnWork { get; set; }

    [ForeignKey("ResArea")]
    [InverseProperty("TEmployees")]
    [Display(Name = "Khu vực phụ trách")]
    public virtual TArea? ResAreaNavigation { get; set; }

    [ForeignKey("ShiftId")]
    [InverseProperty("TEmployees")]
    [Display(Name = " Ca")]
    public virtual TShift? Shift { get; set; }
}
