namespace OdeToHomes.Migrations
{
    using OdeToHomes.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Security;
using WebMatrix.WebData;

    internal sealed class Configuration : DbMigrationsConfiguration<OdeToHomes.Models.OdeToHomesDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(OdeToHomes.Models.OdeToHomesDb context)
        {
            
            //context.FloorPlans.AddOrUpdate(r => r.ModelNum,
            //   new FloorPlan { ModelNum = "A100", Sqft = 512, Beds = 2, Baths = 1, Size = "15' X 40'", Sections = 1, OurPrice = 19999, TheirPrice = 26999, Series = "Rancher" },
            //   new FloorPlan { ModelNum = "B200", Sqft = 1600, Beds = 3, Baths = 2, Size = "30' X 60'", Sections = 2, OurPrice = 39999, TheirPrice = 46999, Series = "Rancher" },
            //   new FloorPlan
            //   {
            //       ModelNum = "C300",
            //       Sqft = 2500,
            //       Beds = 4,
            //       Baths = 3,
            //       Size = "45' X 60'",
            //       Sections = 3,
            //       OurPrice = 59999,
            //       TheirPrice = 76999,
            //       Series = "Ivy Island"

            //   });

            context.Series.AddOrUpdate(r => r.Name,
               new Series
               {
                   Name = "X-DELETED"

               });

            //context.States.AddOrUpdate(r => r.Name,
            //   new State
            //   {
            //       Name = "All",
            //       Serieses =
            //            new List<Series> {
            //                new Series { Name = "Gulf" },
            //                new Series { Name = "Sunset" },
            //                new Series { Name = "Wilston" },
            //                new Series { Name = "Coastal" },
            //                new Series { Name = "Rancher" },
            //                new Series { Name = "Ivy Island" }
            //            }
            //   },
            //   new State
            //   {
            //       Name = "TX",
            //       Serieses =
            //            new List<Series> {
            //                new Series { Name = "Rancher" },
            //                new Series { Name = "Ivy Island" }
            //            }
            //   });
            SeedMembership();
        }
            private void SeedMembership()
            {
                if (!WebSecurity.Initialized)
                {
                    WebSecurity.InitializeDatabaseConnection("DefaultConnection",
                        "UserProfile", "UserId", "UserName", autoCreateTables: true);
                }

                var roles = (SimpleRoleProvider)Roles.Provider;
                var membership = (SimpleMembershipProvider)Membership.Provider;

                if (!roles.RoleExists("SuperAdmin"))
                {
                    roles.CreateRole("SuperAdmin");
                }
                if (membership.GetUser("reniewicki", false) == null)
                {
                    membership.CreateUserAndAccount("reniewicki", "imalittleteapot");
                }
                if (!roles.GetRolesForUser("reniewicki").Contains("SuperAdmin"))
                {
                    roles.AddUsersToRoles(new[] { "reniewicki" }, new[] { "SuperAdmin" });
                } 


                if (membership.GetUser("dubmun", false) == null)
                {
                    membership.CreateUserAndAccount("dubmun", "imalittleteapot");
                }
                if (!roles.GetRolesForUser("dubmun").Contains("SuperAdmin"))
                {
                    roles.AddUsersToRoles(new[] { "dubmun" }, new[] { "SuperAdmin" });
                } 
            }
        
    }
}
