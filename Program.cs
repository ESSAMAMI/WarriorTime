using MartialTime.DBProvider;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MartialTime
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = ".AdventureWorks.Session";
                options.IdleTimeout = TimeSpan.FromHours(2);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            });

            // MySQL Connection...
            string _GetConnectionString = builder.Configuration.GetConnectionString("DefaultConnectionMySQL");
            builder.Services.AddDbContext<WarriortimeContext>(options => options.UseMySql(_GetConnectionString, ServerVersion.AutoDetect(_GetConnectionString)));
            
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
            app.UseAuthorization();
            app.UseSession();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=SignIn}/{action=SignIn}/{id?}");

            app.Run();
        }
    }
}