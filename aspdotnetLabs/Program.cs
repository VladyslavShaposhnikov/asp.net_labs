using aspdotnetLabs.Models;
using aspdotnetLabs.Models.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace aspdotnetLabs;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        //var connectionString = builder.Configuration.GetConnectionString("AppDbContext") ?? throw new InvalidOperationException("Connection string 'AppDbContext' not found.");
        builder.Services.AddRazorPages();  
        // Add services to the container.
        builder.Services.AddControllersWithViews();
        
        builder.Services.AddDbContext<AppDbContext>();
        
        builder.Services.AddDefaultIdentity<IdentityUser>()       // dodać
            .AddRoles<IdentityRole>()                             //
            .AddEntityFrameworkStores<AppDbContext>();  

        //builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AppDbContext>();
        //builder.Services.AddTransient<IBookService, EFBookService>();
        
        //builder.Services.AddSingleton<IBookService, MemoryBookService>();
        
        builder.Services.AddSingleton<IDateTimeProvider, CurrentDateTimeProvider>();

        builder.Services.AddMemoryCache();                        // dodać
        builder.Services.AddSession();  
        
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

        app.UseAuthentication();                                 // dodać
        app.UseAuthorization();                                  // dodać
        app.UseSession();                                        // dodać 
        app.MapRazorPages();     

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        
        app.Run();
    }
}