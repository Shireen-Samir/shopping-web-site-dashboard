using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using task.Models;

namespace task.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products
        [Authorize (Roles ="admin,client")]
        public ActionResult Index()
        {
            var products = db.products.Include(p => p.Category);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        [Authorize(Roles = "admin,client")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.categoryid = new SelectList(db.categories, "categoryid", "categoryname");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "productid,productname,productdescription,price,categoryid")] Product product, HttpPostedFileBase pic)
        {
            if (pic == null)
            {
                return Content("Null pic");
            }
            if (ModelState.IsValid)
            {
                string filename = Guid.NewGuid().ToString();
                string ext = pic.FileName.Split('.')[1];
                pic.SaveAs(Server.MapPath("~/images/product/") + filename + "." + ext);
                product.productpicture = filename + "." + ext;
                db.products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.categoryid = new SelectList(db.categories, "categoryid", "categoryname", product.categoryid);
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.categoryid = new SelectList(db.categories, "categoryid", "categoryname", product.categoryid);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "productid,productname,productdescription,price,categoryid")] Product product, HttpPostedFileBase pic)
        {
            if (ModelState.IsValid)
            {
                Product oldpro = db.products.FirstOrDefault(a => a.productid == product.productid);
                if (oldpro != null)
                {
                    oldpro.productname = product.productname;
                    oldpro.productdescription = product.productdescription;
                    oldpro.price = product.price;
                    oldpro.categoryid = product.categoryid;
                    if (pic == null)
                    {
                        oldpro.productpicture = oldpro.productpicture;
                    }
                    else
                    {
                        string filename = Guid.NewGuid().ToString();
                        string ext = pic.FileName.Split('.')[1];
                        pic.SaveAs(Server.MapPath("~/images/product/") + filename + "." + ext);
                        product.productpicture = filename + "." + ext;
                        oldpro.productpicture = product.productpicture;

                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
               
            }
            ViewBag.categoryid = new SelectList(db.categories, "categoryid", "categoryname", product.categoryid);
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        public ActionResult Delete2(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return PartialView(product);
        }

        // POST: Products/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.products.Find(id);
            db.products.Remove(product);
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
