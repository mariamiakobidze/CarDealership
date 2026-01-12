using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp30.Models;

[Index("CarId", Name = "UQ__Sales__68A0342F348D8D5A", IsUnique = true)]
public partial class Sale
{
    [Key]
    public int SaleId { get; set; }

    public int CarId { get; set; }

    public int CustomerId { get; set; }

    public int EmployeeId { get; set; }

    public DateOnly? SaleDate { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal TotalAmount { get; set; }

    [ForeignKey("CarId")]
    [InverseProperty("Sale")]
    public virtual Car Car { get; set; } = null!;

    [ForeignKey("CustomerId")]
    [InverseProperty("Sales")]
    public virtual Customer Customer { get; set; } = null!;

    [ForeignKey("EmployeeId")]
    [InverseProperty("Sales")]
    public virtual Employee Employee { get; set; } = null!;

    [InverseProperty("Sale")]
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    [ForeignKey("SaleId")]
    [InverseProperty("Sales")]
    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
