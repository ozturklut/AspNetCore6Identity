using System.ComponentModel.DataAnnotations;

namespace Udemy.Identity.Models;

public class UserSignInModel
{
    [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
    public string? Username { get; set; }

    [Required(ErrorMessage = "Şifre alanı zorunludur.")]
    public string? Password { get; set; }
    public bool RememberMe { get; set; }
    public string? ReturnUrl { get; set; }
}