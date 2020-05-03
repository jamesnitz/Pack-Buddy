using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PackBuddy.Models
{
    public class WishList
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public double Price { get; set; }
        public string PurchaseLink { get; set; }
        [Required]
        public string ApplicationuserId { get; set; }
        public ApplicationUser applicationUser { get; set; }
    }
}
