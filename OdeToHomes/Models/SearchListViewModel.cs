using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OdeToHomes.Models
{
    public class SearchListViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Model Number")]
        public string ModelNum { get; set; }
        public int Sqft { get; set; }
        public int Beds { get; set; }
        public int Baths { get; set; }
        public string Size { get; set; }
        public int Sections { get; set; }
        public int OurPrice { get; set; }
        public int TheirPrice { get; set; }
        public string Series { get; set; }
        //public State localState { get; set; }
    }
}