using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public string Notes { get; set; }
        public string ApplicationuserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<GearTrip> GearTrips { get; set; }
    }
}
