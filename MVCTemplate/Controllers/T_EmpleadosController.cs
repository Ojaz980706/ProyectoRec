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
    public class T_EmpleadosController : Controller
    {
        private Modelo db = new Modelo();

        // GET: T_Empleados
        public ActionResult Index()
        {
            var t_Empleados = db.T_Empleados.Include(t => t.T_Areas);
            return View(t_Empleados.ToList());
        }

        // GET: T_Empleados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Empleados t_Empleados = db.T_Empleados.Find(id);
            if (t_Empleados == null)
            {
                return HttpNotFound();
            }
            return View(t_Empleados);
        }

        // GET: T_Empleados/Create
        public ActionResult Create()
        {
            ViewBag.AreaID = new SelectList(db.T_Areas, "IDarea", "NombreArea");
            return View();
        }

        // POST: T_Empleados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NoEmpleado,NombreCompleto,AreaID,Puesto,Cara")] T_Empleados t_Empleados)
        {
            if (ModelState.IsValid)
            {
                db.T_Empleados.Add(t_Empleados);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AreaID = new SelectList(db.T_Areas, "IDarea", "NombreArea", t_Empleados.AreaID);
            return View(t_Empleados);
        }

        // GET: T_Empleados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Empleados t_Empleados = db.T_Empleados.Find(id);
            if (t_Empleados == null)
            {
                return HttpNotFound();
            }
            ViewBag.AreaID = new SelectList(db.T_Areas, "IDarea", "NombreArea", t_Empleados.AreaID);
            return View(t_Empleados);
        }

        // POST: T_Empleados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NoEmpleado,NombreCompleto,AreaID,Puesto,Cara")] T_Empleados t_Empleados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_Empleados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AreaID = new SelectList(db.T_Areas, "IDarea", "NombreArea", t_Empleados.AreaID);
            return View(t_Empleados);
        }

        // GET: T_Empleados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Empleados t_Empleados = db.T_Empleados.Find(id);
            if (t_Empleados == null)
            {
                return HttpNotFound();
            }
            return View(t_Empleados);
        }

        // POST: T_Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_Empleados t_Empleados = db.T_Empleados.Find(id);
            db.T_Empleados.Remove(t_Empleados);
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
