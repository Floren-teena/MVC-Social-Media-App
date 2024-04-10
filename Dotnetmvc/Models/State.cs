using System.ComponentModel.DataAnnotations;

namespace dotnetcoremorningclass.Models
{
    public class State
    {
        [Key]
        public int Id { get; set; }
        public string StateName { get; set; }
        public string StateCode { get; set; }
    }
}
