using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TripProject.Models;

public partial class TbCountry
{
    public int Id { get; set; }
    [Required(ErrorMessage = "please Enter CountryName")]
    [StringLength(50, ErrorMessage = "CountryName must be less than 50")]
    public string? CounturyName { get; set; }

    public virtual ICollection<TbCity> TbCities { get; set; } = new List<TbCity>();
}
