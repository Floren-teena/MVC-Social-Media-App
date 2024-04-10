using dotnetcoremorningclass.Data.Enum;
using dotnetcoremorningclass.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Identity.Client;

namespace dotnetcoremorningclass.Data
{//
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {

            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();   

                //club
                if (!context.clubs.Any())
                {
                    context.clubs.AddRange(new List<Club>
                    {
                        new Club()
                        {
                            Title = "RAve Club 1",
                            Image = "https://getwallpapers.com/wallpaper/full/f/b/a/264290.jpg",
                            Description = "This is a description strange to precious",
                            ClubCategory = ClubCategory.City,
                            Address = new Address()
                            {
                                Street = "elegushi precious",
                                City = "Benin city airport road",
                                State = "Abuja upper"
                            }

                        },


                        new Club()
                        {
                            Title = "RAve Club 2",
                            Image = "https://getwallpapers.com/wallpaper/full/f/b/a/264290.jpg",
                            Description = "This is a description strange to precious",
                            ClubCategory = ClubCategory.City,
                            Address = new Address()
                            {
                                Street = "elegushi ",
                                City = "Benin cityroad",
                                State = "Abuja upper elegushi"
                            }

                        },

                        new Club()
                        {
                            Title = "RAve Club 3",
                            Image = "https://getwallpapers.com/wallpaper/full/f/b/a/264290.jpg",
                            ClubCategory = ClubCategory.City,
                            Address = new Address()
                            {
                                Street = "elegushi abeokuta ",
                                City = "Benin city road constructs",
                                State = "Abuja upper elegushi praaa"
                            }

                        },


                    });

                    context.SaveChanges();
                }

                //Races

                if (!context.Races.Any())
                {
                    context.Races.AddRange(new List<Race>
                    {
                        new Race()
                        {
                            Title = "Running Race 1",
                            Image = "https://getwallpapers.com/wallpaper/full/f/b/a/264290.jpg",
                            Description = "This is a Racing description strange to precious",
                            RaceCategory = RaceCategory.fivek,
                            Address = new Address()
                            {
                                Street = "elegushi precious",
                                City = "Benin city airport road",
                                State = "Abuja upper"
                            }

                        },


                        new Race()
                        {
                            Title = "Running Race 2",
                            Image = "https://getwallpapers.com/wallpaper/full/f/b/a/264290.jpg",
                            Description = "This is a Racing description strange to precious",
                            RaceCategory = RaceCategory.HalfMarathon,
                            Address = new Address()
                            {
                                Street = "elegushi precious2",
                                City = "Benin city airport road",
                                State = "Abuja upper"
                            }

                        },

                        new Race()
                        {
                            Title = "Running Race 3",
                            Image = "https://getwallpapers.com/wallpaper/full/f/b/a/264290.jpg",
                            Description = "This is a Racing description strange to precious",
                            RaceCategory = RaceCategory.Tenk,
                            Address = new Address()
                            {
                                Street = "elegushi precious32",
                                City = "Benin city airport road",
                                State = "Abuja upper"
                            }

                        },


                    });

                    context.SaveChanges();

                }

            }


        }
        public static async Task SeedUserAndRolesAsync(IApplicationBuilder applicationBuilder)
        {

            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Role:it is used to set ur roles
                //User:it is used to set ur user

                //This are the Roles being set,if it is not there
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                    if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

                    if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                


                // User: Admin User
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminEmail = "dammydeji.dd@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "dammydeji",
                        Email = adminEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "123 Ohen Road",
                            City = "BeninCity",
                            State = "EDO"
                        }
                    };

                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }


                //User: Normal User
                string appUserEmail = "bloszomgianem@gmail.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "Blossom",
                        Email = appUserEmail,
                        PhoneNumber = "08111611777",
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "123456 Ohen Road",
                            City = "BeninCity Upper",
                            State = "EDO"
                        }
                    };

                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }

            }
        }

    }
}