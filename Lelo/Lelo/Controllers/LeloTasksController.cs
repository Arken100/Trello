using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Lelo.DAL;
using Lelo.Models;

namespace Lelo.Controllers
{
    public class LeloTasksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LeloTasks
        public ActionResult Index()
        {
            var leloTasks = db.LeloTasks.Include(l => l.TaskList);
            return View(leloTasks.ToList());
        }

        // GET: LeloTasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeloTask leloTask = db.LeloTasks.Find(id);
            if (leloTask == null)
            {
                return HttpNotFound();
            }
            return View(leloTask);
        }

        // GET: LeloTasks/Create
        public ActionResult Create()
        {
            ViewBag.TaskListId = new SelectList(db.TaskLists, "Id", "Name");
            return View();
        }

        // POST: LeloTasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,TaskListId")] LeloTask leloTask)
        {
            if (ModelState.IsValid)
            {
                db.LeloTasks.Add(leloTask);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TaskListId = new SelectList(db.TaskLists, "Id", "Name", leloTask.TaskListId);
            return View(leloTask);
        }

        // GET: LeloTasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeloTask leloTask = db.LeloTasks.Find(id);
            if (leloTask == null)
            {
                return HttpNotFound();
            }
            ViewBag.TaskListId = new SelectList(db.TaskLists, "Id", "Name", leloTask.TaskListId);
            return View(leloTask);
        }

        // POST: LeloTasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,TaskListId")] LeloTask leloTask)
        {
            if (ModelState.IsValid)
            {
                db.Entry(leloTask).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TaskListId = new SelectList(db.TaskLists, "Id", "Name", leloTask.TaskListId);
            return View(leloTask);
        }

        // GET: LeloTasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeloTask leloTask = db.LeloTasks.Find(id);
            if (leloTask == null)
            {
                return HttpNotFound();
            }
            return View(leloTask);
        }

        // POST: LeloTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LeloTask leloTask = db.LeloTasks.Find(id);
            db.LeloTasks.Remove(leloTask);
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
