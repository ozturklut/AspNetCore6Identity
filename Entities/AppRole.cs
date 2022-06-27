using Microsoft.AspNetCore.Identity;

namespace IdentityApp.Entities;

public class AppRole : IdentityRole<int>
{
    public DateTime CreatedTime { get; set; }
}