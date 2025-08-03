using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BidWars.Areas.Identity.Data;
using BidWars.Database;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AuthContextConnection") ?? throw new InvalidOperationException("Connection string 'AuthContextConnection' not found.");;

builder.Services.AddDbContext<AuthContext>(options => options.UseSqlServer(connectionString));

// In this case, the AuthDbContext connection string is the same as the BidWarsContext connection string.
// However, if they were different, then I would create a separate variable for the the BidWarsContext connection string.
builder.Services.AddDbContext<BidWarsContext>(options => options.UseSqlServer(connectionString));

builder.Services
    .AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AuthContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages();

app.Run();
