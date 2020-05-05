using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PackBuddy.Models
{
    public class GearType
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Label { get; set; }
        public string ImagePath { get; set; }
    }
}
