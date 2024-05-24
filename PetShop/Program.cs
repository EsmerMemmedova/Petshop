using Business.Services.Abstarcts;
using Business.Services.Concretes;
using Core.Models;
using Core.RepostoryAbstarcts;
using Data.DAL;
using Data.ReopostoryConcretes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.Password.RequireNonAlphanumeric = true;
    opt.Password.RequiredLength = 8;
 
    opt.User.RequireUniqueEmail = false;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.AddScoped<ISliderService,SliderService>();
builder.Services.AddScoped<ISliderRepostory,SliderRepostory>();
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
