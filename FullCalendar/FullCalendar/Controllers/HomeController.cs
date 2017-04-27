using FullCalendar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FullCalendar.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetEvents()
        {
            //Here MyDatabaseEntities is our entity datacontext (see Step 4)
            using (EntitiesOne dc = new EntitiesOne())
            {
                var v = dc.Events.OrderBy(a => a.StartAt).ToList();
                return new JsonResult { Data = v, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        // Input Event To Calendar
        public ActionResult Event()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Event([Bind(Include = "EventID,Title,Description,StartAt,EndAt,IsFullDay,")] Events events)
        {
            if (ModelState.IsValid)
            {
                Events eventsOne = new Events
                {
                    Title = events.Title,
                    Description = events.Description,
                    StartAt = events.StartAt,
                    EndAt = events.EndAt,
                    IsFullDay = events.IsFullDay,
                };
                db.Events.Add(eventsOne);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(events);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}