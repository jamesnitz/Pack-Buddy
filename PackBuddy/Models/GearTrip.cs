using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PackBuddy.Models
{
    public class GearTrip
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int GearId { get; set; }
        public Gear Gear { get; set; }
        [Required]
        public int TripId { get; set; }
        public Trip Trip { get; set; }
    }
}
