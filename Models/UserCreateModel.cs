using System.ComponentModel.DataAnnotations;

namespace Udemy.Identity.Models;

public class UserCreateModel
{
    [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
    public string? Username { get; set; }

    [EmailAddress(ErrorMessage = "Lütfen bir email formatı giriniz.")]
    [Required(ErrorMessage = "Email adresi zorunludur.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Parola alanı zorunludur.")]
    public string? Password { get; set; }

    [Compare("Password", ErrorMessage = "Paralolar eşleşmiyor.")]
    public string? ConfirmPassword { get; set; }

    [Required(ErrorMessage = "Cinsiyet alanı zorunludur.")]
    public string? Gender { get; set; }
}