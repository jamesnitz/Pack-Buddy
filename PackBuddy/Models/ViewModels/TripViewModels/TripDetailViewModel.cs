using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PackBuddy.Models.ViewModels.TripViewModels
{
    public class TripDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Notes { get; set; }
        public string ApplicationuserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public List <GearTrip> GearTrips { get; set; }
    }
}
