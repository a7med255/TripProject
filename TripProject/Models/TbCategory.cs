using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TripProject.Models;

public partial class TbCategory
{
    public int Id { get; set; }
    [Required(ErrorMessage = "please Enter CategoryName")]
    [StringLength(50, ErrorMessage = "CategoryName must be less than 50")]
    public string? CategoryName { get; set; }

    public virtual ICollection<TbTrip> TbTrips { get; set; } = new List<TbTrip>();
}
