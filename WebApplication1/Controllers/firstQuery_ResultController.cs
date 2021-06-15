using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1;

namespace WebApplication1.Controllers
{
    public class firstQuery_ResultController : Controller
    {
        private ShopEntities db = new ShopEntities();

        // GET: firstQuery_Result
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.numbers = new SelectList(db.Shops.Select(shop => shop.number), new int[0]);
            return View(new List<firstQuery_Result>());
        }

        [HttpPost]
        public ActionResult Index(List<int> numbers)
        {
            ViewBag.numbers = new SelectList(db.Shops.Select(shop => shop.number), numbers);
            List<firstQuery_Result> results = new List<firstQuery_Result>();
            foreach (int number in numbers)
            {
                results.AddRange(db.firstQuery(number).ToList());
            }            
            return View(results);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
