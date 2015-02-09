using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OdeToHomes.Models
{
    public class State
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Series> Serieses { get; set; } //Yes I know Serieses is not a word
    }
}