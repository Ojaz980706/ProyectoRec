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
    public class T_RolesController : Controller
    {
        private Modelo db = new Modelo();

        // GET: T_Roles
        public ActionResult Index()
        {
            return View(db.T_Roles.ToList());
        }

        // GET: T_Roles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Roles t_Roles = db.T_Roles.Find(id);
            if (t_Roles == null)
            {
                return HttpNotFound();
            }
            return View(t_Roles);
        }

        // GET: T_Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: T_Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDrol,NombreRol")] T_Roles t_Roles)
        {
            if (ModelState.IsValid)
            {
                db.T_Roles.Add(t_Roles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(t_Roles);
        }

        // GET: T_Roles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Roles t_Roles = db.T_Roles.Find(id);
            if (t_Roles == null)
            {
                return HttpNotFound();
            }
            return View(t_Roles);
        }

        // POST: T_Roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDrol,NombreRol")] T_Roles t_Roles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_Roles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t_Roles);
        }

        // GET: T_Roles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Roles t_Roles = db.T_Roles.Find(id);
            if (t_Roles == null)
            {
                return HttpNotFound();
            }
            return View(t_Roles);
        }

        // POST: T_Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_Roles t_Roles = db.T_Roles.Find(id);
            db.T_Roles.Remove(t_Roles);
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
