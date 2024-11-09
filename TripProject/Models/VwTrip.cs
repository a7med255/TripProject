using System;
using System.Collections.Generic;

namespace TripProject.Models;

public partial class VwTrip
{
    public string? CategoryName { get; set; }

    public string? CityName { get; set; }

    public int? CounturyId { get; set; }

    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int Price { get; set; }

    public string? Image { get; set; }

    public int? CategoryId { get; set; }

    public int? CityId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? BestSellerRequest { get; set; }
}
