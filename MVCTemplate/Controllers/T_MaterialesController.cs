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
    public class T_MaterialesController : Controller
    {
        private Modelo db = new Modelo();

        // GET: T_Materiales
        public ActionResult Index()
        {
            return View(db.T_Materiales.ToList());
        }

        // GET: T_Materiales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Materiales t_Materiales = db.T_Materiales.Find(id);
            if (t_Materiales == null)
            {
                return HttpNotFound();
            }
            return View(t_Materiales);
        }

        // GET: T_Materiales/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: T_Materiales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDmaterial,Nombre,Marca,Modelo")] T_Materiales t_Materiales)
        {
            if (ModelState.IsValid)
            {
                db.T_Materiales.Add(t_Materiales);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(t_Materiales);
        }

        // GET: T_Materiales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Materiales t_Materiales = db.T_Materiales.Find(id);
            if (t_Materiales == null)
            {
                return HttpNotFound();
            }
            return View(t_Materiales);
        }

        // POST: T_Materiales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDmaterial,Nombre,Marca,Modelo")] T_Materiales t_Materiales)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_Materiales).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t_Materiales);
        }

        // GET: T_Materiales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Materiales t_Materiales = db.T_Materiales.Find(id);
            if (t_Materiales == null)
            {
                return HttpNotFound();
            }
            return View(t_Materiales);
        }

        // POST: T_Materiales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_Materiales t_Materiales = db.T_Materiales.Find(id);
            db.T_Materiales.Remove(t_Materiales);
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
