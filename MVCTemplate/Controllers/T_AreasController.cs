using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCTemplate.Models.ModelosCamara;

namespace MVCTemplate.Controllers
{
    public class T_AreasController : Controller
    {
        private Modelo db = new Modelo();

        // GET: T_Areas
        public ActionResult Index()
        {
            return View(db.T_Areas.ToList());
        }

        // GET: T_Areas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Areas t_Areas = db.T_Areas.Find(id);
            if (t_Areas == null)
            {
                return HttpNotFound();
            }
            return View(t_Areas);
        }

        // GET: T_Areas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: T_Areas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDarea,NombreArea")] T_Areas t_Areas)
        {
            if (ModelState.IsValid)
            {
                db.T_Areas.Add(t_Areas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(t_Areas);
        }

        // GET: T_Areas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Areas t_Areas = db.T_Areas.Find(id);
            if (t_Areas == null)
            {
                return HttpNotFound();
            }
            return View(t_Areas);
        }

        // POST: T_Areas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDarea,NombreArea")] T_Areas t_Areas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_Areas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t_Areas);
        }

        // GET: T_Areas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Areas t_Areas = db.T_Areas.Find(id);
            if (t_Areas == null)
            {
                return HttpNotFound();
            }
            return View(t_Areas);
        }

        // POST: T_Areas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_Areas t_Areas = db.T_Areas.Find(id);
            db.T_Areas.Remove(t_Areas);
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
