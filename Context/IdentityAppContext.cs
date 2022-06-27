using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IdentityApp.Entities;

namespace IdentityApp.Context;

public class IdentityAppContext : IdentityDbContext<AppUser, AppRole, int>
{
    public IdentityAppContext(DbContextOptions<IdentityAppContext> options) : base(options)
    {

    }
}