using System.ComponentModel.DataAnnotations;

namespace Udemy.Identity.Models;

public class RoleCreateModel
{
    [Required(ErrorMessage = "Ad alanı zorunludur.")]
    public string? Name { get; set; }
}