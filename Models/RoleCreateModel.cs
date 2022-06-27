using System.ComponentModel.DataAnnotations;

namespace IdentityApp.Models;

public class RoleCreateModel
{
    [Required(ErrorMessage = "Ad alanı zorunludur.")]
    public string? Name { get; set; }
}