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
    public class LeloTasksController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LeloTasks
        public ActionResult Index()
        {
            var uid = GetCurrentUserId();
            var leloTasks = db.LeloTasks.Include(l => l.TaskList).Include(l => l.TaskList.Board).Where(x => !x.IsDeleted)
             .Where(x => x.TaskList.Board.UserId == uid || x.TaskList.Board.Team.Users.Select(xx => xx.Id).Contains(CurrentUserId));

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

            ViewBag.BoardId = db.TaskLists.Find(leloTask.TaskListId).BoardId.Value;

            return View(leloTask);
        }

        // GET: LeloTasks/Create
        public ActionResult Create()
        {
            ViewBag.TaskListId = new SelectList(db.TaskLists, "Id", "Name");
            return View();
        }

        public ActionResult AddTask(int listId, int boardId)
        {
            ViewBag.TaskListId = listId;
            ViewBag.BoardId = boardId;

            return View();
        }

        [HttpPost]
        public ActionResult AddTask(LeloTask leloTask)
        {
            Create(leloTask);
            int boardId = db.Boards.Where(x => x.TaskLists.Select(xx => xx.Id).ToList().Contains(leloTask.TaskListId.Value)).FirstOrDefault().Id;

            return RedirectToAction("Details","Boards", new {Id = boardId});
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

        [HttpPost]
        //        [ValidateAntiForgeryToken]
        public ActionResult UpdateList(int taskId, int listId, string[] order)
        {

            LeloTask leloTask = db.LeloTasks.Where(x => x.Id == taskId).FirstOrDefault();
            leloTask.TaskListId = listId;
            db.Entry(leloTask).State = EntityState.Modified;
            db.SaveChanges();


            if (order != null)
            {
                for (int i = 0; i < order.Length; i++)
                {
                    int currentTaskId = int.Parse(order[i]);
                    var task = db.LeloTasks.Where(x => x.Id == currentTaskId).FirstOrDefault(); ;
                    task.Position = i;
                    db.Entry(task).State = EntityState.Modified;
                }
                db.SaveChanges();
            }



            return new HttpStatusCodeResult(HttpStatusCode.OK);
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
            //db.LeloTasks.Remove(leloTask);
            leloTask.IsDeleted = true;
            db.Entry(leloTask).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteTask(int? id, int boardId)
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

            ViewBag.BoardId = boardId;

            return View(leloTask);
        }

        // POST: LeloTasks/Delete/5
        [HttpPost, ActionName("DeleteTask")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTaskConfirmed(int id)
        {
            LeloTask leloTask = db.LeloTasks.Find(id);
            //db.LeloTasks.Remove(leloTask);
            leloTask.IsDeleted = true;
            db.Entry(leloTask).State = EntityState.Modified;
            db.SaveChanges();

            int boardId = db.Boards.Where(x => x.TaskLists.Select(xx => xx.Id).ToList().Contains(leloTask.TaskListId.Value)).FirstOrDefault().Id;
            return RedirectToAction("Details", "Boards", new { Id = boardId });
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
