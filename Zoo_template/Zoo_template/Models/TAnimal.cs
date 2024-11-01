using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Zoo_template.Models;

[Table("tAnimal")]
public partial class TAnimal
{
    [Key]
    [Column("AnimalID")]
    public int AnimalId { get; set; }

    [StringLength(100)]
    [Display(Name = "Tên tiếng việt ")]
    [Required(ErrorMessage = "Vui lòng nhập tên thú")]
    public string? Name { get; set; }

    [StringLength(100)]
    [Display(Name = "Tên khoa học ")]
    public string? ScienName { get; set; }
    [Display(Name = "Thời gian vào ")]
    [Required(ErrorMessage = "Vui lòng cho biết thời gian vào chuồng")]

    public DateOnly? TimeIn { get; set; }
    [Display(Name = "Thơi gian rời chuồng ")]
    public DateOnly? TimeOut { get; set; }
    [Display(Name = "Tuổi ")]
    [Required(ErrorMessage = "Vui lòng cho biết tuổi")]
    public int? Age { get; set; }
  
    [Column("SpeciesID")]
    [Display(Name = "Loài ")]
    [Required(ErrorMessage = "Vui lòng cho biết loài")]
    public int? SpeciesId { get; set; }

    [Column("CageID")]
    public int? CageId { get; set; }

    [StringLength(10)]
    [Display(Name = "Giới tính ")]
    [Required(ErrorMessage = "Vui lòng cho biết giới tính")]
    public string? Gender { get; set; }

    [StringLength(255)]
    [Display(Name = "Ảnh")]
    
    public string? Image { get; set; }

    [Column("FoodID")]
    [Display(Name = "Loại thức ăn ")]
    [Required(ErrorMessage = "Vui lòng cho biết loại thức ăn")]
    public int? FoodId { get; set; }

    [ForeignKey("CageId")]
    [InverseProperty("TAnimals")]
    [Display(Name = "Mã chuồng ")]
   
    public virtual TCage? Cage { get; set; }

    [ForeignKey("FoodId")]
    [InverseProperty("TAnimals")]
    [Display(Name = "Thức ăn ")]
    public virtual TFood? Food { get; set; }

    [ForeignKey("SpeciesId")]
    [InverseProperty("TAnimals")]
    [Display(Name = "Loài ")]
    public virtual TSpecy? Species { get; set; }
}
