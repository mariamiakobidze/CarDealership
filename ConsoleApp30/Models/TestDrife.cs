using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp30.Models;

public partial class TestDrife
{
    [Key]
    public int TestDriveId { get; set; }

    public int CarId { get; set; }

    public int CustomerId { get; set; }

    public DateOnly TestDriveDate { get; set; }

    [ForeignKey("CarId")]
    [InverseProperty("TestDrives")]
    public virtual Car Car { get; set; } = null!;

    [ForeignKey("CustomerId")]
    [InverseProperty("TestDrives")]
    public virtual Customer Customer { get; set; } = null!;
}
