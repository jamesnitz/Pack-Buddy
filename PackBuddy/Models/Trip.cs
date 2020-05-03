using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PackBuddy.Models
{
    public class Trip
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public string Notes { get; set; }
        [Required]
        public string ApplicationuserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public List<GearTrip> GearTrips { get; set; }
    }
}
