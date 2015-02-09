using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OdeToHomes.Models
{
    public class OdeToHomesDb : DbContext
    {
        public OdeToHomesDb()
            : base("name=DefaultConnection")
        {

        }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<FloorPlan> FloorPlans { get; set; }

        public DbSet<State> States { get; set; }

        public DbSet<Series> Series { get; set; }



    }
}