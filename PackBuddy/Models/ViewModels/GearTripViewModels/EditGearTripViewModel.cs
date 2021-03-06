﻿using PackBuddy.Models.ViewModels.GearViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PackBuddy.Models.ViewModels.GearTripViewModels
{
    public class EditGearTripViewModel
    {
        public int Id { get; set; }
        public string ApplicationuserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public List<Gear> AlreadySelected { get; set; }
        public List<GearViewModel> AddedGears { get; set; }

        public Trip Trip { get; set; }
    }
}
