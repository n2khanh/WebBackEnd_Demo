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
    public string? Name { get; set; }

    [StringLength(100)]
    public string? ScienName { get; set; }

    public DateOnly? TimeIn { get; set; }

    public DateOnly? TimeOut { get; set; }

    public int? Age { get; set; }

    [Column("SpeciesID")]
    public int? SpeciesId { get; set; }

    [Column("CageID")]
    public int? CageId { get; set; }

    [StringLength(10)]
    public string? Gender { get; set; }

    [StringLength(255)]
    public string? Image { get; set; }

    [Column("FoodID")]
    public int? FoodId { get; set; }

    [ForeignKey("CageId")]
    [InverseProperty("TAnimals")]
    public virtual TCage? Cage { get; set; }

    [ForeignKey("FoodId")]
    [InverseProperty("TAnimals")]
    public virtual TFood? Food { get; set; }

    [ForeignKey("SpeciesId")]
    [InverseProperty("TAnimals")]
    public virtual TSpecy? Species { get; set; }
}
