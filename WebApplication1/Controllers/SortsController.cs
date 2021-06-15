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
    public class SortsController : Controller
    {
        private ShopEntities db = new ShopEntities();

        // GET: Sorts
        public ActionResult Index()
        {
            return View(db.Sorts.ToList());
        }

        // GET: Sorts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sort sort = db.Sorts.Find(id);
            if (sort == null)
            {
                return HttpNotFound();
            }
            return View(sort);
        }

        // GET: Sorts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sorts/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name")] Sort sort)
        {
            if (db.Sorts.Select(db_sort => db_sort.name).Contains(sort.name))
            {
                ModelState.AddModelError("name", $"Сорт с названием {sort.name} уже существует!");
            }

            if (ModelState.IsValid)
            {
                db.Sorts.Add(sort);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sort);
        }

        // GET: Sorts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sort sort = db.Sorts.Find(id);
            if (sort == null)
            {
                return HttpNotFound();
            }
            return View(sort);
        }

        // POST: Sorts/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name")] Sort sort)
        {
            var sameNameSorts = db.Sorts.AsNoTracking().Where(db_sort => db_sort.name == sort.name);
            if (sameNameSorts.Any() && sameNameSorts.First().id != sort.id)
            {
                ModelState.AddModelError("name", $"Сорт с названием {sort.name} уже существует!");
            }

            if (ModelState.IsValid)
            {
                db.Entry(sort).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sort);
        }

        // GET: Sorts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sort sort = db.Sorts.Find(id);
            if (sort == null)
            {
                return HttpNotFound();
            }
            return View(sort);
        }

        // POST: Sorts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sort sort = db.Sorts.Find(id);
            db.Sorts.Remove(sort);
            db.SaveChanges();
            return RedirectToAction("Index");
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
