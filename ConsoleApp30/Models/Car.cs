using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp30.Models;

[Index("Vin", Name = "UQ__Cars__C5DF234C72CED089", IsUnique = true)]
public partial class Car
{
    [Key]
    public int CarId { get; set; }

    [Column("VIN")]
    [StringLength(17)]
    public string Vin { get; set; } = null!;

    public int ModelId { get; set; }

    public int? Year { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }

    [StringLength(20)]
    public string? Status { get; set; }

    [ForeignKey("ModelId")]
    [InverseProperty("Cars")]
    public virtual Model Model { get; set; } = null!;

    [InverseProperty("Car")]
    public virtual Sale? Sale { get; set; }

    [InverseProperty("Car")]
    public virtual ICollection<TestDrife> TestDrives { get; set; } = new List<TestDrife>();
}
