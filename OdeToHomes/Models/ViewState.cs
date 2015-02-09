using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OdeToHomes.Models
{
    public class ViewState
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Series> Serieses { get; set; }
        public int NumberOfSeries { get; set; }
        public ICollection<Series> AvailableSeries { get; set; }
        public ICollection<Series> SelectedSeries { get; set; }
        public string[] PostedSeries { get; set; }
    }
}