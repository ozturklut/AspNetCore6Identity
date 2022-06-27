using System.ComponentModel.DataAnnotations;

namespace IdentityApp.Models;

public class UserAdminCreateModel
{
    [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "Email adresi zorunludur.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Cinsiyet alanı zorunludur.")]
    public string? Gender { get; set; }
}