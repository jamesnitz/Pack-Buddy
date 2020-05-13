using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PackBuddy.Models.ViewModels.GearViewModels
{
    public class GearFormViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        [Display(Name = "Gear Type")]
        public int GearTypeId { get; set; }
        [Display(Name = "Rate 1-5")]
        public int Rating { get; set; }
        [Required]
        public string Condition { get; set; }
        public IFormFile ImagePath { get; set; }
        public string ImageString { get; set; }
        public List<SelectListItem> GearTypeOptions { get; set; }
    }
}
