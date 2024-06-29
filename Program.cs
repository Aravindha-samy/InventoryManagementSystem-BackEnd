
using InventoryManagementSystem.CommonUtility;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog;
using System.Text.Json.Serialization;


namespace InventoryManagementSystem
{
    public static class Program
    {
        static  ProductLogger logger=new ProductLogger();
        
        public static void Main(string[] args)
        {

            logger.BuildConfigure();
            Log.Information("Inventory Management System Application Started");
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();            //    Options =>
            //Appliction Service
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddCors(options=>
            {
                options.AddPolicy("AllowSpecificOrigin", builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });

            });
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/Login/Login";
            });
            builder.Services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.ConstraintMap.Add("PositiveConstraint", typeof(PositiveConstraintModel));
                });            
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddDistributedMemoryCache();
            
            builder.Services.AddSession(options =>
            { 
                options.IdleTimeout = TimeSpan.FromSeconds(300);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            builder.Services.AddControllersWithViews()
               .AddJsonOptions(options =>
               {
                   options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
               });



            var app = builder.Build();
                

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors("AllowSpecificOrigin");
            
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Product}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
