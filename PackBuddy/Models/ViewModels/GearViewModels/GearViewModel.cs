using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PackBuddy.Models.ViewModels.GearViewModels
{
    public class GearViewModel
    {
        public Gear Gear { get; set; }
        public bool SelectedGear { get; set; }
        public int TripId { get; set; }
    }
}
