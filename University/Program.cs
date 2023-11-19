using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using University.Data;

namespace University
{
    public class Program
    {
        public IConfiguration _configuration;
        public Program(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });

            builder.Services.AddIdentity<Users, IdentityRole>().AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();
            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters += " ";
            });
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
            });

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/Error/Index";
                    await next();
                }
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            app.Run();
        }
    }
}