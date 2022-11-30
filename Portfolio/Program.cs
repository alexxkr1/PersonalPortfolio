using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Portfolio.Data.Repository;
using Volta.Data;
using Volta.Models;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Data.FileManager;

var builder = WebApplication.CreateBuilder(args);
var host = WebApplication.CreateBuilder(args);
//var scope = IServiceProvider.CreateScope();
//var context = scope.ServiceProvider.GetService<PortfolioDbContext>();
//var userMgr = scope.ServiceProvider.GetService<UserManager<IdentityUser>>();
//var roleMgr = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

//context.Database.EnsureCreated();

//var adminRole= new IdentityRole("Admin");
//if(!context.Roles.Any())
//{
//    // Create a role
//    roleMgr.CreateAsync(adminRole).GetAwaiter().GetResult();
//}
//if (!context.Users.Any(u => u.UserName == "admin"))
//{
//    // CREATE AN ADMIN
//    var adminUser = new IdentityUser
//    {
//        UserName = "admin",
//        Email = "admin@test.com"
//    };
//    var result = userMgr.CreateAsync(adminUser, "password").GetAwaiter().GetResult();
//    // add role to user
//    userMgr.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();
//}

var connectionString = builder.Configuration.GetConnectionString("PortfolioDbContextConnection") ?? throw new InvalidOperationException("Connection string 'PortfolioDbContextConnection' not found.");

builder.Services.AddDbContext<PortfolioDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<PortfolioUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
})
    .AddEntityFrameworkStores<PortfolioDbContext>();
    //.AddRoles<IdentityRole>();

//
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("ApplicationDbContextConnection")
    ));


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IRepository, Repository>(); 
builder.Services.AddTransient<IFilemanager, FileManager>(); 

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
app.UseStaticFiles();
app.Run();
