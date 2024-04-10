using dotnetcoremorningclass.Data;
using dotnetcoremorningclass.Helpers;
using dotnetcoremorningclass.Interfaces;
using dotnetcoremorningclass.Models;
using dotnetcoremorningclass.Repository;
using dotnetcoremorningclass.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //how to get services for context from your database
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            
            builder.Services.AddScoped<IClubRepository, ClubRepository >();
            builder.Services.AddScoped<IRaceRepository, RaceRepository>();
            builder.Services.AddScoped<IPhotoService, PhotoService>();
            builder.Services.AddScoped<IDashBoardRepository, DashBoardRepository>();

            //when getting services from appsettings.json,u use the builder.configuration.Getsection
            builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

            //bcus we are using Identity as a service and we are still making use of our dbcontext
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            //example is reddies,its used to store ur tokens as it is a temporary memory storage
            builder.Services.AddMemoryCache();
            //for mapping user sessions while using d app
            builder.Services.AddSession();
            //to enable cookies to be in charge of the authentication on the front end
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

            var app = builder.Build();

            //seed once
            if (args.Length == 1 && args[0].ToLower() == "seeddata")
            {
                Seed.SeedData(app);
                await Seed.SeedUserAndRolesAsync(app);
            }

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

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
      
    

