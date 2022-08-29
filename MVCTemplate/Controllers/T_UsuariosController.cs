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
    public class T_UsuariosController : Controller
    {
        private Modelo db = new Modelo();

        // GET: T_Usuarios
        public ActionResult Index()
        {
            var t_Usuarios = db.T_Usuarios.Include(t => t.T_Empleados).Include(t => t.T_Roles);
            return View(t_Usuarios.ToList());
        }

        // GET: T_Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Usuarios t_Usuarios = db.T_Usuarios.Find(id);
            if (t_Usuarios == null)
            {
                return HttpNotFound();
            }
            return View(t_Usuarios);
        }

        // GET: T_Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.EmpleadoID = new SelectList(db.T_Empleados, "NoEmpleado", "NombreCompleto");
            ViewBag.Rol = new SelectList(db.T_Roles, "IDrol", "NombreRol");
            return View();
        }

        // POST: T_Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDusuario,EmpleadoID,Rol,Contraseña,contraseñaRostro")] T_Usuarios t_Usuarios)
        {
            if (ModelState.IsValid)
            {
                db.T_Usuarios.Add(t_Usuarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmpleadoID = new SelectList(db.T_Empleados, "NoEmpleado", "NombreCompleto", t_Usuarios.EmpleadoID);
            ViewBag.Rol = new SelectList(db.T_Roles, "IDrol", "NombreRol", t_Usuarios.Rol);
            return View(t_Usuarios);
        }

        // GET: T_Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Usuarios t_Usuarios = db.T_Usuarios.Find(id);
            if (t_Usuarios == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmpleadoID = new SelectList(db.T_Empleados, "NoEmpleado", "NombreCompleto", t_Usuarios.EmpleadoID);
            ViewBag.Rol = new SelectList(db.T_Roles, "IDrol", "NombreRol", t_Usuarios.Rol);
            return View(t_Usuarios);
        }

        // POST: T_Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDusuario,EmpleadoID,Rol,Contraseña,contraseñaRostro")] T_Usuarios t_Usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_Usuarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmpleadoID = new SelectList(db.T_Empleados, "NoEmpleado", "NombreCompleto", t_Usuarios.EmpleadoID);
            ViewBag.Rol = new SelectList(db.T_Roles, "IDrol", "NombreRol", t_Usuarios.Rol);
            return View(t_Usuarios);
        }

        // GET: T_Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Usuarios t_Usuarios = db.T_Usuarios.Find(id);
            if (t_Usuarios == null)
            {
                return HttpNotFound();
            }
            return View(t_Usuarios);
        }

        // POST: T_Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_Usuarios t_Usuarios = db.T_Usuarios.Find(id);
            db.T_Usuarios.Remove(t_Usuarios);
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
