    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PackBuddy.Models
{
    public class Gear
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        [Required]
        public int GearTypeId { get; set; }
        public GearType GearType { get; set; }
        public int Rating { get; set; }
        [Required]
        public string Condition { get; set; }
        public string ImagePath { get; set; }
        [Required]
        public string ApplicationuserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public List<GearTrip> GearTrips { get; set; }

    }
}
