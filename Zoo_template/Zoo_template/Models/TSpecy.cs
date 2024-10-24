using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Zoo_template.Models;

[Table("tSpecies")]
public partial class TSpecy
{
    [Key]
    [Column("SpeciesID")]
    public int SpeciesId { get; set; }

    [StringLength(100)]
    public string? SpeciesName { get; set; }

    [StringLength(255)]
    public string? Note { get; set; }

    [InverseProperty("Species")]
    public virtual ICollection<TAnimal> TAnimals { get; set; } = new List<TAnimal>();
}
