using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SportComplexApp.Data;
using SportComplexApp.Data.Models;
using SportComplexApp.Data.Repository.Interfaces;
using SportComplexApp.Services.Data.Contracts;
using SportComplexApp.Services;
using SportComplexApp.Services.Mapping;
using SportComplexApp.Web.ViewModels;

var builder = WebApplication.CreateBuilder(args);
string connectionString = builder.Configuration.GetConnectionString("SQLServer");
// Add services to the container.
builder.Services
    .AddDbContext<SportComplexDbContext>(options =>
    options.UseSqlServer(connectionString));

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

builder.Services.ConfigureApplicationCookie(cfg =>
cfg.LoginPath = "/Identity/Account/Login");

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ISportService, SportService>();


var app = builder.Build();

AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).Assembly);

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
