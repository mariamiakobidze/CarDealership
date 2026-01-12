using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp30.Models;

[Index("Name", Name = "UQ__Manufact__737584F6D6A35E25", IsUnique = true)]
public partial class Manufacturer
{
    [Key]
    public int ManufacturerId { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(50)]
    public string Country { get; set; } = null!;

    [InverseProperty("Manufacturer")]
    public virtual ICollection<Model> Models { get; set; } = new List<Model>();
}
