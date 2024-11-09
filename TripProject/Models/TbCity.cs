using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TripProject.Models;

public partial class TbCity
{
    public int Id { get; set; }
    [Required(ErrorMessage = "please Enter CityName")]
    [StringLength(50, ErrorMessage = "CityName must be less than 50")]
    public string? CityName { get; set; }

    public int? CounturyId { get; set; }

    public virtual TbCountry? Countury { get; set; }

    public virtual ICollection<TbTrip> TbTrips { get; set; } = new List<TbTrip>();
}
