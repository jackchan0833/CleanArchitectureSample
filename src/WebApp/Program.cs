using CleanArchitectureSample.ApplicationCore.Interfaces.Repository;
using CleanArchitectureSample.ApplicationCore.Interfaces.Service;
using CleanArchitectureSample.ApplicationCore.Services;
using CleanArchitectureSample.Infrastructure;
using CleanArchitectureSample.Infrastructure.Repository;
using CleanArchitectureSample.WebApp.Utls;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Runtime.CompilerServices;

namespace CleanArchitectureSample.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //add db context
            //builder.Services.AddDbContext<SampleDbContext>();
            builder.Services.AddDbContext<SampleDbContext>(options =>
            {
                //options.UseSqlite("Data Source=:memory:");
                options.UseSqlite("Data Source=./Bin/test.db");
            });

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ICodeItemService, CodeItemService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ICodeItemRepository, CodeItemRepository>();
            builder.Services.AddScoped<Utility, Utility>();

            var app = builder.Build();
            SeedData(app); // add seed data

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
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

        private static void SeedData(WebApplication app)
        {
            using (var dbContext = app.Services.CreateScope().ServiceProvider.GetService<SampleDbContext>())
            {
                if (dbContext.CodeItems.Count() > 0)
                {
                    return;
                }
                // add code items
                dbContext.CodeItems.Add(new Infrastructure.Entities.CodeItem() { CodeId = "100", Category = "Gender", CodeName = "M", CodeValue = "Male", SeqNo = 1 });
                dbContext.CodeItems.Add(new Infrastructure.Entities.CodeItem() { CodeId = "101", Category = "Gender", CodeName = "F", CodeValue = "Female", SeqNo = 2 });
                dbContext.CodeItems.Add(new Infrastructure.Entities.CodeItem() { CodeId = "102", Category = "Gender", CodeName = "U", CodeValue = "Unknown", SeqNo = 3 });
                dbContext.CodeItems.Add(new Infrastructure.Entities.CodeItem() { CodeId = "200", Category = "UserStatus", CodeName = "ACT", CodeValue = "Active", SeqNo = 1 });
                dbContext.CodeItems.Add(new Infrastructure.Entities.CodeItem() { CodeId = "201", Category = "UserStatus", CodeName = "INA", CodeValue = "In Active", SeqNo = 2 });
                dbContext.CodeItems.Add(new Infrastructure.Entities.CodeItem() { CodeId = "202", Category = "UserStatus", CodeName = "TER", CodeValue = "Terminate", SeqNo = 3 });
                dbContext.CodeItems.Add(new Infrastructure.Entities.CodeItem() { CodeId = "203", Category = "UserStatus", CodeName = "EXP", CodeValue = "Expired", SeqNo = 4 });

                // add users
                dbContext.Users.Add(new Infrastructure.Entities.User() { UserId = System.Guid.NewGuid().ToString(), UserName = "john", NickName = "John Donbuter", Email = "john@gmail.com", Status = "ACT", Address = string.Empty });
                dbContext.Users.Add(new Infrastructure.Entities.User() { UserId = System.Guid.NewGuid().ToString(), UserName = "tom", NickName = "Tom Wisten", Email = "tom@gmail.com", Status = "ACT", Address = string.Empty });
                dbContext.Users.Add(new Infrastructure.Entities.User() { UserId = System.Guid.NewGuid().ToString(), UserName = "jim", NickName = "Jim Loton", Email = "jim@gmail.com", Status = "INA", Address = string.Empty });
                dbContext.Users.Add(new Infrastructure.Entities.User() { UserId = System.Guid.NewGuid().ToString(), UserName = "lucy", NickName = "Lucy Osca", Email = "lucy@gmail.com", Status = "INA", Address = string.Empty });
                dbContext.Users.Add(new Infrastructure.Entities.User() { UserId = System.Guid.NewGuid().ToString(), UserName = "harry", NickName = "Harry Ministe", Email = "harry@gmail.com", Status = "TER" , Address = string.Empty });
                dbContext.SaveChanges();
            }
        }
    }
}