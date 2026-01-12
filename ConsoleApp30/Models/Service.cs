using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp30.Models;

[Index("Name", Name = "UQ__Services__737584F6475642E3", IsUnique = true)]
public partial class Service
{
    [Key]
    public int ServiceId { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column(TypeName = "decimal(8, 2)")]
    public decimal? Price { get; set; }

    [ForeignKey("ServiceId")]
    [InverseProperty("Services")]
    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
