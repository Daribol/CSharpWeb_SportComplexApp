using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SportComplexApp.Data;
using SportComplexApp.Data.Models;

var builder = WebApplication.CreateBuilder(args);
string connectionString = builder.Configuration.GetConnectionString("SQLServer");
// Add services to the container.
builder.Services
    .AddDbContext<SportComplexDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

builder.Services
    .AddDefaultIdentity<Client>(option =>
    {
        option.SignIn.RequireConfirmedAccount = false;
        option.Password.RequireDigit = true;
        option.Password.RequiredLength = 6;
        option.Password.RequireNonAlphanumeric = false;
        option.Password.RequireUppercase = true;
        option.Password.RequireLowercase = true;
        option.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<SportComplexDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
