using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PackBuddy.Models.ViewModels.SharedGearsViewModels
{
    public class SharedGearIndexViewModel
    {
       
        public int Id { get; set; }
        public int GearId { get; set; }
        public Gear Gear { get; set; }
        public string ApplicationuserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string RequestMessage { get; set; }
        public bool AcceptedRequest { get; set; }
        public List<Gear> FriendGears { get; set; }
    }
}
