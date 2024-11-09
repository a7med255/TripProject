using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TripProject.Models;

public partial class TbTrip
{
    public int Id { get; set; }
    [Required(ErrorMessage = "please Enter TripName")]
    [StringLength(50, ErrorMessage = "TripName must be less than 50")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "please Enter Description")]
    [StringLength(2000, ErrorMessage = "Description must be less than 2000")]
    public string? Description { get; set; }
    [Required(ErrorMessage = "please Enter Price")]
    public int Price { get; set; }
    [Required(ErrorMessage = "please Enter Image")]
    public string? Image { get; set; }

    public int? CategoryId { get; set; }

    public int? CityId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? BestSellerRequest { get; set; }

    public virtual TbCategory? Category { get; set; }

    public virtual TbCity? City { get; set; }

    public virtual ICollection<TbRequest> TbRequests { get; set; } = new List<TbRequest>();
}
