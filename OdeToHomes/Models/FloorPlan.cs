using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OdeToHomes.Models
{
    public class FloorPlan
    {
        public int Id { get; set; }
        [Display(Name = "Model Number")]
        public string ModelNum { get; set; }
        [Display(Name = "Sqft")]
        public int Sqft { get; set; }
        [Display(Name = "Beds")]
        public int Beds { get; set; }
        [Display(Name = "Baths")]
        public int Baths { get; set; }
        [Display(Name = "Size")]
        public string Size { get; set; }
        [Display(Name = "Sections")]
        public int Sections { get; set; }
        [Display(Name = "Our Price")]
        public int OurPrice { get; set; }
        [Display(Name = "Their Price")]
        public int TheirPrice { get; set; }
        [Display(Name = "Series")]
        public Series SeriesLabel { get; set; }
    }
}