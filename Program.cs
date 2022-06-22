using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Udemy.Identity.Context;
using Udemy.Identity.CustomDescriber;
using Udemy.Identity.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
    // opt.Password.RequireDigit = false;
    // opt.Password.RequiredLength = 1;
    // opt.Password.RequireLowercase = false;
    // opt.Password.RequireUppercase = false;
    // opt.Password.RequireNonAlphanumeric = false;
    opt.Lockout.MaxFailedAccessAttempts = 10;
}).AddErrorDescriber<CustomErrorDescriber>().AddEntityFrameworkStores<UdemyContext>();

builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.Cookie.HttpOnly = true;
    opt.Cookie.SameSite = SameSiteMode.Strict;
    opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    opt.Cookie.Name = "Cookie";
    opt.ExpireTimeSpan = TimeSpan.FromDays(25);
    opt.LoginPath = new PathString("/Home/SignIn");
    opt.AccessDeniedPath = new PathString("/Home/AccessDenied");
});

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<UdemyContext>(opt =>
    {
        opt.UseSqlServer("Server=127.0.0.1,1433;Database=IdentityDB;Trust Server Certificate=true; User=sa; Password=842862432156.sql;");
        opt.LogTo(Console.WriteLine, LogLevel.Information);
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    RequestPath = "/node_modules",
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "node_modules"))
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();
