using DataTables.AspNet.AspNetCore;
using Net8CoreMVC.Services;

namespace Net8CoreMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHttpClient<CallApiService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5217/api/Emp");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.RegisterDataTables();
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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Employees}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
