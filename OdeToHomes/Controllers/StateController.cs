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
    [Authorize(Roles="SuperAdmin")]
    public class StateController : Controller
    {
        private OdeToHomesDb _db = new OdeToHomesDb();

        //
        // GET: /State/

        public ActionResult Index()
        {
            var model =
                _db.States
                .OrderBy(r => r.Name)
                .Select(r => new ViewState
                {
                    Id = r.Id,
                    Name = r.Name,
                    NumberOfSeries = r.Serieses.Count()

                });

            return View(model);
        }

        

        private MultiSelectList GetSeries(string[] selectedValues)
        {

            List<Series> selectSeries = new List<Series>();
            List<Series> selectSeries2 = new List<Series>();
            int i = 0;

            var seriesModel = from t in _db.Series
                              orderby t.Name
                              where t.Name != "X-DELETED"
                              select t;

            foreach (var item in seriesModel)
            {
                selectSeries.Add(item);

            }

            if (selectedValues != null)
            {
                foreach (var item in selectSeries)
                {
                    foreach (var sItem in selectedValues)
                    {
                        Int32.TryParse(sItem, out i);
                        if (i == item.Id)
                        {
                            selectSeries2.Add(item);
                        }
                    }
                }
            }
            else
            {
                selectSeries2 = selectSeries;
            }


            return new MultiSelectList(selectSeries2, "Id", "Name", selectedValues);

        }

        private ICollection<Series> GetSeries2(string[] selectedSeries)
        {
            ICollection<Series> seriesList = new List<Series>();

            var seriesModel = from t in _db.Series
                              orderby t.Name
                              where t.Name != "X-DELETED"
                              select t;

            foreach (var item in seriesModel)
            {
                seriesList.Add(item);

            }

            return seriesList;
        }

        //
        // GET: /State/Create

        public ActionResult Create()
        {
            ViewState checkBox = new ViewState();
            checkBox.AvailableSeries = GetSeries2(null);

            return View(checkBox);

            //ViewData["listSeries"] = GetSeries(null);
            //return View();
        }

        //
        // POST: /State/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewState viewState)
        {


            if (ModelState.IsValid)
            {
                State newState = new State();
                List<Series> newSeries = new List<Series>();
                int i = 0;

                newState.Name = viewState.Name;

                foreach ( var item in viewState.PostedSeries)
                {
                    Int32.TryParse(item, out i);
                    
                    newSeries.Add(_db.Series.Find(i));
                }

                newState.Serieses = newSeries;

                
                _db.States.Add(newState);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(viewState);
        }

        //
        // GET: /State/Edit/5

        public ActionResult Edit(int id = 0)
        {

            

            State state = _db.States.Find(id);
            if (state == null)
            {
                return HttpNotFound();
            }


            ViewState checkBox = new ViewState();
            checkBox.AvailableSeries = GetSeries2(null);
            checkBox.SelectedSeries = state.Serieses;
            checkBox.Name = state.Name;
            checkBox.Id = state.Id;

            return View(checkBox);
        }

        //
        // POST: /State/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ViewState viewState)
        {

            if (ModelState.IsValid)
            {
                State newState = _db.States.Find(viewState.Id);
                List<Series> newSeries = new List<Series>();
                int i = 0;

                newState.Serieses.Clear();

                foreach (var item in viewState.PostedSeries)
                {
                    Int32.TryParse(item, out i);

                    newSeries.Add(_db.Series.Find(i));
                }

                newState.Serieses = newSeries;



                _db.Entry(newState).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(viewState);
        }

        //
        // GET: /State/Delete/5

        public ActionResult Delete(int id = 0)
        {
            State state = _db.States.Find(id);
            if (state == null)
            {
                return HttpNotFound();
            }
            return View(state);
        }

        //
        // POST: /State/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            State state = _db.States.Find(id);

            state.Serieses.Clear();

            _db.States.Remove(state);
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