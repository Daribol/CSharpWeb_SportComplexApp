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
using SportComplexApp.Services.Data;
using SportComplexApp.Data.Configuration;

var builder = WebApplication.CreateBuilder(args);
string? connectionString = builder.Configuration.GetConnectionString("SQLServer");
// Add services to the container.
builder.Services
    .AddDbContext<SportComplexDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services
    .AddIdentity<Client, IdentityRole>(option =>
    {
        option.SignIn.RequireConfirmedAccount = false;
        option.Password.RequireDigit = true;
        option.Password.RequiredLength = 6;
        option.Password.RequireNonAlphanumeric = false;
        option.Password.RequireUppercase = true;
        option.Password.RequireLowercase = true;
        option.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<SportComplexDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

builder.Services.ConfigureApplicationCookie(cfg =>
cfg.LoginPath = "/Identity/Account/Login");

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ISportService, SportService>();
builder.Services.AddScoped<ISpaService, SportComplexApp.Services.Data.SpaService>();
builder.Services.AddScoped<ITournamentService, TournamentService>();
builder.Services.AddScoped<ITrainerService, TrainerService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFacilityService, FacilityService>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    DatabaseSeeder.SeedRoles(services);
    DatabaseSeeder.AssignAdminRole(services);
}

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

app.UseStatusCodePagesWithRedirects("/Home/Error/{0}");

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "Errors",
    pattern: "{controller=Home}/{action=Index}/{statusCode?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
