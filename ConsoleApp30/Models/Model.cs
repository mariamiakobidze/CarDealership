using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp30.Models;

public partial class Model
{
    [Key]
    public int ModelId { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    public int ManufacturerId { get; set; }

    [InverseProperty("Model")]
    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();

    [ForeignKey("ManufacturerId")]
    [InverseProperty("Models")]
    public virtual Manufacturer Manufacturer { get; set; } = null!;
}
