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
    public class CategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Categories
        [Authorize(Roles = "admin,client")]
        public ActionResult Index()
        {
            return View(db.categories.ToList());
        }

        // GET: Categories/Details/5
        [Authorize(Roles = "admin,client")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "categoryid,categoryname,categorydescription,categorypicture")] Category category, HttpPostedFileBase pic)
        {
            if (pic == null)
            {
                return Content("Null pic");
            }
            if (ModelState.IsValid)
            {
                string filename = Guid.NewGuid().ToString();
                string ext = pic.FileName.Split('.')[1];
                pic.SaveAs(Server.MapPath("~/images/category/") + filename + "." + ext);
                category.categorypicture = filename + "." + ext;
                db.categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "categoryid,categoryname,categorydescription")] Category category ,HttpPostedFileBase pic)
        {
            
            if (ModelState.IsValid)
            {
                Category oldcat = db.categories.FirstOrDefault(a => a.categoryid == category.categoryid);
                if(oldcat !=null)
                {
                    oldcat.categoryname = category.categoryname;
                    oldcat.categorydescription = category.categorydescription;
                    if (pic == null)
                    {
                        oldcat.categorypicture = oldcat.categorypicture;
                    }
                    else
                    {
                        string filename = Guid.NewGuid().ToString();
                        string ext = pic.FileName.Split('.')[1];
                        pic.SaveAs(Server.MapPath("~/images/category/") + filename + "." + ext);
                        category.categorypicture = filename + "." + ext;
                        oldcat.categorypicture = category.categorypicture;
                      
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");


                }  
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete2(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return PartialView(category);
        }


        // POST: Categories/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.categories.Find(id);
            db.categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "admin,client")]
        public ActionResult products(int id)
        {
            Category cat = db.categories.FirstOrDefault(a => a.categoryid == id);
            var product = from pro in db.products
                          where pro.categoryid == id
                          select pro;

            ViewBag.categoryname = cat.categoryname;
            ViewBag.catid = cat.categoryid;
            return View(product.ToList());
        }

        [Authorize(Roles = "admin")]
        public ActionResult Createproduct()
        {
            
            ViewBag.categoryid= new SelectList(db.categories, "categoryid", "categoryname");
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Createproduct([Bind(Include = "productid,productname,productdescription,price,categoryid")] Product product, HttpPostedFileBase pic)
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
