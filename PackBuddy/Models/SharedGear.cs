using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PackBuddy.Models
{
    public class SharedGear
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int GearId { get; set; }
        public Gear Gear { get; set; }
        [Required]
        public string ApplicationuserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string RequestMessage { get; set; }
        [Required]
        public bool AcceptedRequest { get; set; }
    }
}
