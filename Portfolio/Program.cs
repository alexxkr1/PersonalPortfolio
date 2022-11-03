using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Portfolio.Data;
using Portfolio.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PortfolioDbContextConnection") ?? throw new InvalidOperationException("Connection string 'PortfolioDbContextConnection' not found.");

builder.Services.AddDbContext<PortfolioDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<PortfolioUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<PortfolioDbContext>();

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
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
