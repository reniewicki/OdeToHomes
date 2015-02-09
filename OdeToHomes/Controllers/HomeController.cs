using OdeToHomes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using System.Web.Services.WebMethod;
//using System.Web.Script.Services.ScriptMethod;

namespace OdeToHomes.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Photos()
        {

            return View();
        }

        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod]  
        public static AjaxControlToolkit.Slide[] GetImages()
        {
            return new AjaxControlToolkit.Slide[] {   
        new AjaxControlToolkit.Slide("Images/Pics/C3602.jpg", "Blue Hills", "Go Blue"),  
        new AjaxControlToolkit.Slide("Images/Pics/C2062.jpg", "Sunset", "Setting sun"),  
        new AjaxControlToolkit.Slide("Images/Pics/C361.jpg", "Winter", "Wintery..."),  
        new AjaxControlToolkit.Slide("Images/Pics/C360.jpg", "Water lillies", "Lillies in the water"),  
        new AjaxControlToolkit.Slide("Images/Pics/C305.jpg", "Sedona", "Portrait style picture")};
        } 

        public ActionResult About()
        {
            var model = new AboutModel();
            model.Name = "Ode to Homes";
            model.Location = "St George UT";

            return View(model);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
    }
}