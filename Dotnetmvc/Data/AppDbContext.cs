using dotnetcoremorningclass.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace dotnetcoremorningclass.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Address> Addresss { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Club> clubs { get; set; }
        public virtual DbSet<Race> Races { get; set; }
        public virtual DbSet<State> States { get; set; }


    }
}
