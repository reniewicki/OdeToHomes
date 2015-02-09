using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OdeToHomes.Models;
using System.Web.Security;

namespace OdeToHomes.Controllers
{
    public class FloorPlanController : Controller
    {
        private OdeToHomesDb _db = new OdeToHomesDb();

        public ActionResult Autocomplete(string term)
        {
            var model =
                _db.FloorPlans
                   .Where(r => r.ModelNum.StartsWith(term))
                   .Take(10)
                   .Select(r => new
                   {
                       label = r.ModelNum
                   });

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult Index(int id = 0, string searchTerm = null)
        {
            List<string> viewSeries = new List<string>();

            int userID = 0;

            if (!User.IsInRole("SuperAdmin"))
            {
                if (User.Identity.IsAuthenticated)
                {
                    userID = (int)Membership.GetUser(User.Identity.Name).ProviderUserKey;
                }

                UserProfile currentUser = _db.UserProfiles.Find(userID);

                id = currentUser.state;

                if (id == 0)
                {
                    return RedirectToAction("StateSelect", "FloorPlan");
                }

                State stateItem = _db.States.Find(id);
                if (stateItem == null)
                {
                    return HttpNotFound();
                }

                foreach (var item in stateItem.Serieses)
                {
                    viewSeries.Add(item.Name);
                }
                ViewData["listSeries"] = viewSeries;
                ViewBag.messageString = stateItem.Name;
            }
            else
            {

                var seriesModel = from t in _db.Series
                                  orderby t.Name
                                  select t;

                foreach (var item in seriesModel)
                {
                    viewSeries.Add(item.Name);

                }

                ViewData["listSeries"] = viewSeries;
                ViewBag.messageString = "ALL";
            }

            var model =
                _db.FloorPlans
                .OrderBy(r => r.Sqft)
                .Where(r => searchTerm == null || r.ModelNum.StartsWith(searchTerm))
                //.Where(r => searchSeries == null || r.Series == searchSeries)
                .Select(r => new SearchListViewModel
                {
                    Id = r.Id,
                    ModelNum = r.ModelNum,
                    Sqft = r.Sqft,
                    Beds = r.Beds,
                    Baths = r.Baths,
                    Size = r.Size,
                    Sections = r.Sections,
                    OurPrice = r.OurPrice,
                    TheirPrice = r.TheirPrice,
                    Series = r.SeriesLabel.Name,

                });

            if (Request.IsAjaxRequest())
            {
                return PartialView("_FloorPlanSeries", model);
            }
            
            return View(model);
        }

        public ActionResult StateSelect()
        {


            var query = _db.States.Select(c => new { c.Id, c.Name });
            ViewData["listStates"] = new SelectList(query.AsEnumerable(), "Id", "Name");

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StateSelect(FormCollection selectedState)
        {
            int userID = 0;

            string selected = selectedState["Id"];
            int i = 0;

            Int32.TryParse(selected, out i);

            if (User.Identity.IsAuthenticated)
            {
                userID = (int)Membership.GetUser(User.Identity.Name).ProviderUserKey;
            }

            UserProfile currentUser = _db.UserProfiles.Find(userID);

            currentUser.state = i;

            _db.Entry(currentUser).State = EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("Index", "FloorPlan");
            
            
        }

        //
        // GET: /FloorPlan/Details/5

        public ActionResult Details(int id = 0)
        {
            FloorPlan floorplan = _db.FloorPlans.Find(id);
            if (floorplan == null)
            {
                return HttpNotFound();
            }

            return View(floorplan);
        }


        private List<SelectListItem> GetSeries()
        {
            List<SelectListItem> viewSeries = new List<SelectListItem>();

            var series = from r in _db.Series
                         orderby r.Name
                         select r;

            foreach (var item in series)
            {
                viewSeries.Add(new SelectListItem { Text = item.Name });
            }

            return viewSeries;
        }

        //
        // GET: /FloorPlan/Create

        public ActionResult Create()
        {
            

            ViewData["listSeries"] = GetSeries();

            return View();
        }

        //
        // POST: /FloorPlan/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FloorPlan floorplan, FormCollection selectedSeries)
        {
            if (ModelState.IsValid)
            {
                string label = selectedSeries["listSeries"];

                var series = from r in _db.Series
                             orderby r.Name
                             where r.Name == label
                             select r;

                foreach (var item in series)
                {
                    
                        floorplan.SeriesLabel = item;
                    
                }

                _db.FloorPlans.Add(floorplan);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(floorplan);
        }

        //
        // GET: /FloorPlan/Edit/5

        public ActionResult Edit(int id = 0)
        {
            var query = _db.Series.Select(c => new { c.Id, c.Name });
            ViewData["listSeries"] = new SelectList(query.AsEnumerable(), "Id", "Name");

            //ViewData["listSeries"] = GetSeries();

            FloorPlan floorplan = _db.FloorPlans.Find(id);
            if (floorplan == null)
            {
                return HttpNotFound();
            }
            return View(floorplan);
        }

        //
        // POST: /FloorPlan/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FloorPlan floorplan, FormCollection selectedSeries)
        {
            if (ModelState.IsValid)
            {
                string label = selectedSeries["SeriesId"];
                int i = 0;

                Int32.TryParse(label, out i);



                //FloorPlan floorplanEdited = _db.FloorPlans.Find(floorplan.Id);
                //if (floorplanEdited == null)
                //{
                //    return HttpNotFound();
                //}

                //floorplanEdited.SeriesLabel = _db.Series.Find(i);


                //_db.Entry(floorplan).State = EntityState.Modified;
                //_db.SaveChanges();

                FloorPlan floorplanEdited = _db.FloorPlans.Find(floorplan.Id);
                if (floorplanEdited == null)
                {
                    return HttpNotFound();
                }

                floorplanEdited.Baths = floorplan.Baths;
                floorplanEdited.Beds = floorplan.Beds;
                floorplanEdited.ModelNum = floorplan.ModelNum;
                floorplanEdited.OurPrice = floorplan.OurPrice;
                floorplanEdited.TheirPrice = floorplan.TheirPrice;
                floorplanEdited.Sections = floorplan.Sections;
                floorplanEdited.Size = floorplan.Size;
                floorplanEdited.Sqft = floorplan.Sqft;

                floorplanEdited.SeriesLabel = _db.Series.Find(i);


                _db.Entry(floorplanEdited).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(floorplan);
        }

        //
        // GET: /FloorPlan/Delete/5

        public ActionResult Delete(int id = 0)
        {
            FloorPlan floorplan = _db.FloorPlans.Find(id);
            if (floorplan == null)
            {
                return HttpNotFound();
            }
            return View(floorplan);
        }

        
        
         //POST: /FloorPlan/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FloorPlan floorplan = _db.FloorPlans.Find(id);
            _db.FloorPlans.Remove(floorplan);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Archive(int id = 0)
        {
            FloorPlan floorplan = _db.FloorPlans.Find(id);
            if (floorplan == null)
            {
                return HttpNotFound();
            }
            return View(floorplan);
        }



        //POST: /FloorPlan/Delete/5

        [HttpPost, ActionName("Archive")]
        [ValidateAntiForgeryToken]
        public ActionResult ArchiveConfirmed(int id)
        {
            FloorPlan floorplan = _db.FloorPlans.Find(id);

            var deletedSeries =
                from r1 in _db.Series
                orderby r1.Name
                where r1.Name == "X-DELETED"
                select r1;

            int deletedId = 0;

            foreach (var item1 in deletedSeries)
            {
                deletedId = item1.Id;
            }

            floorplan.SeriesLabel = _db.Series.Find(deletedId);
            _db.Entry(floorplan).State = EntityState.Modified;

            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}