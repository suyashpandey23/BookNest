using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookNest.Models.ViewModels;

public class RegisterVM {
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    [Display(Name="Confirm Password")]
    public string ConfirmPassword { get; set; }
    
    [Required] 
    public string? Name{ get; set; } = string.Empty;
    
    [Display(Name = "Street Address")]
    public string? StreetAddress { get; set; } = string.Empty;
    
    public string? City { get; set; } = string.Empty;
    
    public string? State { get; set; } = string.Empty;
    
    [Display(Name = "Postal Code")]
    public string? PostalCode { get; set; } = string.Empty;
    
    [Display(Name = "Phone number")]
    public string? PhoneNumber{ get; set; }
    
    public string? Role { get; set; }
    
    [ValidateNever]
    public IEnumerable<SelectListItem> RoleList{ get; set; }
}