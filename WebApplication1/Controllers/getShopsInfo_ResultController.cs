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
    public class getShopsInfo_ResultController : Controller
    {
        private ShopEntities db = new ShopEntities();

        // GET: getShopsInfo_Result
        public ActionResult Index()
        {
            return View(db.getShopsInfo().ToList());
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
