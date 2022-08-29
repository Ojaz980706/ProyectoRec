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
    public class T_TransaccionesController : Controller
    {
        private Modelo db = new Modelo();

        // GET: T_Transacciones
        public ActionResult Index()
        {
            var t_Transacciones = db.T_Transacciones.Include(t => t.T_Empleados).Include(t => t.T_Materiales);
            return View(t_Transacciones.ToList());
        }

        // GET: T_Transacciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Transacciones t_Transacciones = db.T_Transacciones.Find(id);
            if (t_Transacciones == null)
            {
                return HttpNotFound();
            }
            return View(t_Transacciones);
        }

        // GET: T_Transacciones/Create
        public ActionResult Create()
        {
            ViewBag.EmpleadoID = new SelectList(db.T_Empleados, "NoEmpleado", "NombreCompleto");
            ViewBag.Material_ID = new SelectList(db.T_Materiales, "IDmaterial", "Nombre");
            return View();
        }

        // POST: T_Transacciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDtransaccion,FechayHora,Material_ID,EmpleadoID,Cantidad")] T_Transacciones t_Transacciones)
        {
            if (ModelState.IsValid)
            {
                db.T_Transacciones.Add(t_Transacciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmpleadoID = new SelectList(db.T_Empleados, "NoEmpleado", "NombreCompleto", t_Transacciones.EmpleadoID);
            ViewBag.Material_ID = new SelectList(db.T_Materiales, "IDmaterial", "Nombre", t_Transacciones.Material_ID);
            return View(t_Transacciones);
        }

        // GET: T_Transacciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Transacciones t_Transacciones = db.T_Transacciones.Find(id);
            if (t_Transacciones == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmpleadoID = new SelectList(db.T_Empleados, "NoEmpleado", "NombreCompleto", t_Transacciones.EmpleadoID);
            ViewBag.Material_ID = new SelectList(db.T_Materiales, "IDmaterial", "Nombre", t_Transacciones.Material_ID);
            return View(t_Transacciones);
        }

        // POST: T_Transacciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDtransaccion,FechayHora,Material_ID,EmpleadoID,Cantidad")] T_Transacciones t_Transacciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_Transacciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmpleadoID = new SelectList(db.T_Empleados, "NoEmpleado", "NombreCompleto", t_Transacciones.EmpleadoID);
            ViewBag.Material_ID = new SelectList(db.T_Materiales, "IDmaterial", "Nombre", t_Transacciones.Material_ID);
            return View(t_Transacciones);
        }

        // GET: T_Transacciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Transacciones t_Transacciones = db.T_Transacciones.Find(id);
            if (t_Transacciones == null)
            {
                return HttpNotFound();
            }
            return View(t_Transacciones);
        }

        // POST: T_Transacciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_Transacciones t_Transacciones = db.T_Transacciones.Find(id);
            db.T_Transacciones.Remove(t_Transacciones);
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
