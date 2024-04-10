using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;

namespace dotnetcoremorningclass.Models
{
    public class AppUser : IdentityUser
    {
        /* [Key]
            public int Id { get; set; }*/

        //identity automatically adds some extra field for us
        //so these fields are the extra fields that it doesn't add
        //for us so we have to pu them our selves and migrate


        public int? Pace { get; set; }
        public int? Mileage { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        public ICollection<Club> Clubs { get; set; }
        public ICollection<Race> Races { get; set; }
    }
}
