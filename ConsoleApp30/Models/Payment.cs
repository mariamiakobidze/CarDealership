using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp30.Models;

public partial class Payment
{
    [Key]
    public int PaymentId { get; set; }

    public int SaleId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Amount { get; set; }

    [StringLength(20)]
    public string? PaymentMethod { get; set; }

    public DateOnly? PaymentDate { get; set; }

    [ForeignKey("SaleId")]
    [InverseProperty("Payments")]
    public virtual Sale Sale { get; set; } = null!;
}
