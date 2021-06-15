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
    public class Product_in_shopController : Controller
    {
        private ShopEntities db = new ShopEntities();

        // GET: Product_in_shop
        public ActionResult Index()
        {
            var product_in_shop = db.Product_in_shop.Include(p => p.Product).Include(p => p.Shop);
            return View(product_in_shop.ToList());
        }

        // GET: Product_in_shop/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_in_shop product_in_shop = db.Product_in_shop.Find(id);
            if (product_in_shop == null)
            {
                return HttpNotFound();
            }
            return View(product_in_shop);
        }

        // GET: Product_in_shop/Create
        public ActionResult Create()
        {            
            ViewBag.Products_id = new SelectList(db.Products, "id", "name");
            ViewBag.Shop_number = new SelectList(db.Shops, "number", "name");
            return View();
        }

        // POST: Product_in_shop/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Products_id,Shop_number,cost,quantity,unit")] Product_in_shop product_in_shop)
        {
            var existingProducts = db.Product_in_shop.Where(db_productInShop =>
                db_productInShop.Products_id == product_in_shop.Products_id &&
                db_productInShop.Shop_number == product_in_shop.Shop_number);

            if (existingProducts.Any())
            {
                string errorMessage = $"Выбранный товар уже продаётся в выбранном магазине!";
                ModelState.AddModelError("Products_id", errorMessage);
                ModelState.AddModelError("Shop_number", errorMessage);
            }

            if (ModelState.IsValid)
            {
                db.Product_in_shop.Add(product_in_shop);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Products_id = new SelectList(db.Products, "id", "name", product_in_shop.Products_id);
            ViewBag.Shop_number = new SelectList(db.Shops, "number", "name", product_in_shop.Shop_number);
            return View(product_in_shop);
        }

        // GET: Product_in_shop/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_in_shop product_in_shop = db.Product_in_shop.Find(id);
            if (product_in_shop == null)
            {
                return HttpNotFound();
            }
            ViewBag.Products_id = new SelectList(db.Products, "id", "name", product_in_shop.Products_id);
            ViewBag.Shop_number = new SelectList(db.Shops, "number", "name", product_in_shop.Shop_number);
            return View(product_in_shop);
        }

        // POST: Product_in_shop/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Products_id,Shop_number,cost,quantity,unit")] Product_in_shop product_in_shop)
        {
            var existingProducts = db.Product_in_shop.AsNoTracking().Where(db_productInShop =>
                db_productInShop.Products_id == product_in_shop.Products_id &&
                db_productInShop.Shop_number == product_in_shop.Shop_number);

            if (existingProducts.Any() && existingProducts.First().id != product_in_shop.id)
            {
                string errorMessage = $"Выбранный товар уже продаётся в выбранном магазине!";
                ModelState.AddModelError("Products_id", errorMessage);
                ModelState.AddModelError("Shop_number", errorMessage);
            }

            if (ModelState.IsValid)
            {
                db.Entry(product_in_shop).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Products_id = new SelectList(db.Products, "id", "name", product_in_shop.Products_id);
            ViewBag.Shop_number = new SelectList(db.Shops, "number", "name", product_in_shop.Shop_number);
            return View(product_in_shop);
        }

        // GET: Product_in_shop/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_in_shop product_in_shop = db.Product_in_shop.Find(id);
            if (product_in_shop == null)
            {
                return HttpNotFound();
            }
            return View(product_in_shop);
        }

        // POST: Product_in_shop/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product_in_shop product_in_shop = db.Product_in_shop.Find(id);
            db.Product_in_shop.Remove(product_in_shop);
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
