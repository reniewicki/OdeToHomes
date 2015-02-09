using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OdeToHomes.Models;

namespace OdeToHomes.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class SeriesController : Controller
    {
        private OdeToHomesDb _db = new OdeToHomesDb();

        //
        // GET: /Series/

        public ActionResult Index()
        {
            return View(_db.Series.ToList());
        }

        public ActionResult StateIndex(int id = 0)
        {
            var state = _db.States.Find(id);
            if (state != null)
            {
                return View(state);
            }
            return HttpNotFound();

        }



        //
        // GET: /Series/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Series/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Series series)
        {
            if (ModelState.IsValid)
            {
                _db.Series.Add(series);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(series);
        }

        //
        // GET: /Series/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Series series = _db.Series.Find(id);
            if (series == null)
            {
                return HttpNotFound();
            }
            return View(series);
        }

        //
        // POST: /Series/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Series series)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(series).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(series);
        }

        //
        // GET: /Series/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Series series = _db.Series.Find(id);
            if (series == null)
            {
                return HttpNotFound();
            }
            return View(series);
        }

        //
        // POST: /Series/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var deletedPlans =
                from r in _db.FloorPlans
                orderby r.Sqft
                where r.SeriesLabel.Id == id
                select r;
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

            foreach (var item in deletedPlans)
            {
                FloorPlan floorplanEdited = _db.FloorPlans.Find(item.Id);
                floorplanEdited.SeriesLabel = _db.Series.Find(deletedId);
                _db.Entry(floorplanEdited).State = EntityState.Modified;
                
            }

            Series series = _db.Series.Find(id);
            _db.Series.Remove(series);
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