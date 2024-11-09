using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TripProject.Models;

public partial class TbRequest
{
    [ValidateNever] 
    public int Id { get; set; }
    [Required(ErrorMessage = "please Enter Name")]
    [StringLength(50, ErrorMessage = "name must be less than 50")]
    public string? Name { get; set; }
    [EmailAddress(ErrorMessage = "please enter valid email")]
    [Remote(controller: "Home", action: "VerifyEmail", ErrorMessage = "email found")]
    [StringLength(50, ErrorMessage = "email must be less than 50")]
    public string Gmail { get; set; } = null!;
    [Required(ErrorMessage = "please Enter Phone")]
    [StringLength(11, ErrorMessage = "phone must be equal 11")]
    [RegularExpression(@"^01[0-5]\d{8}$",
        ErrorMessage = "Please enter a valid phone number.")]
    [Remote(action: "IsPhoneUnique", controller: "Home", ErrorMessage = "Phone number already exists")]
    public string Phone { get; set; } = null!;
    [ValidateNever]
    [Required(ErrorMessage = "please Enter Image")]
    public string Image { get; set; } = null!;

    public int? RequestId { get; set; }

    public DateTime CreateDate { get; set; }

    public virtual TbTrip? TbTrips { get; set; }
}
