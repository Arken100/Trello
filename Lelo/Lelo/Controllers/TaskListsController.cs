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
    public class TaskListsController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TaskLists
        public ActionResult Index()
        {
            var uid = GetCurrentUserId();

            var taskLists = db.TaskLists.Where(x => !x.IsDeleted).Include(t => t.Board)
                .Where(x => x.Board.UserId == uid || x.Board.Team.Users.Select(xx => xx.Id).Contains(CurrentUserId))                
                .ToList();
            return View(taskLists.ToList());
        }

        // GET: TaskLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskList taskList = db.TaskLists.Find(id);
            if (taskList == null)
            {
                return HttpNotFound();
            }
            return View(taskList);
        }

        // GET: TaskLists/Create
        public ActionResult Create()
        {
            ViewBag.BoardId = new SelectList(db.Boards, "Id", "Title");
            return View();
        }

        public ActionResult AddList(int boardId)
        {
            ViewBag.BoardId = boardId;
            ViewBag.BoardId = new SelectList(db.Boards, "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddList([Bind(Include = "Id,Name,Description,BoardId")] TaskList taskList)
        {
            if (ModelState.IsValid)
            {
                int? position = db.TaskLists.Where(x => x.BoardId == taskList.BoardId).Max(x => x.Position);

                taskList.Position = position.HasValue ? position.Value + 1 : 0;
                db.TaskLists.Add(taskList);
                db.SaveChanges();
                return RedirectToAction("Details", "Boards", new { Id = taskList.BoardId });
            }

            ViewBag.BoardId = new SelectList(db.Boards, "Id", "Title", taskList.BoardId);
            return View(taskList);
        }


        // POST: TaskLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,BoardId")] TaskList taskList)
        {
            if (ModelState.IsValid)
            {
                db.TaskLists.Add(taskList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BoardId = new SelectList(db.Boards, "Id", "Title", taskList.BoardId);
            return View(taskList);
        }

        // GET: TaskLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskList taskList = db.TaskLists.Find(id);
            if (taskList == null)
            {
                return HttpNotFound();
            }
            ViewBag.BoardId = new SelectList(db.Boards, "Id", "Title", taskList.BoardId);
            return View(taskList);
        }

        // POST: TaskLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,BoardId")] TaskList taskList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taskList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BoardId = new SelectList(db.Boards, "Id", "Title", taskList.BoardId);
            return View(taskList);
        }

        public ActionResult EditList(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskList taskList = db.TaskLists.Find(id);
            if (taskList == null)
            {
                return HttpNotFound();
            }
            var uid = GetCurrentUserId();
            ViewBag.BoardId = new SelectList(db.Boards
                .Where(x=> !x.IsDeleted), "Id", "Title", taskList.BoardId);
            return View(taskList);
        }

        // POST: TaskLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditList([Bind(Include = "Id,Name,Description,BoardId")] TaskList taskList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taskList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Boards", new { Id = taskList.BoardId });
            }
            ViewBag.BoardId = new SelectList(db.Boards, "Id", "Title", taskList.BoardId);
            return View(taskList);
        }


        // GET: TaskLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskList taskList = db.TaskLists.Find(id);
            if (taskList == null)
            {
                return HttpNotFound();
            }
            return View(taskList);
        }

        // POST: TaskLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaskList taskList = db.TaskLists.Find(id);
            //db.TaskLists.Remove(taskList);
            taskList.IsDeleted = true;
            db.Entry(taskList).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteList(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskList taskList = db.TaskLists.Find(id);
            if (taskList == null)
            {
                return HttpNotFound();
            }
            return View(taskList);
        }

        // POST: TaskLists/Delete/5
        [HttpPost, ActionName("DeleteList")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteListConfirmed(int id)
        {
            TaskList taskList = db.TaskLists.Find(id);
            //db.TaskLists.Remove(taskList);

            foreach (LeloTask task in taskList.LeloTasks)
            {
                task.IsDeleted = true;
                db.Entry(task).State = EntityState.Modified;
            }
            taskList.IsDeleted = true;
            db.Entry(taskList).State = EntityState.Modified;


            taskList.IsDeleted = true;
            db.Entry(taskList).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Details", "Boards", new { Id = taskList.BoardId });
        }

        [HttpPost]
        //        [ValidateAntiForgeryToken]
        public ActionResult UpdateListOrder(int boardId, string[] order)
        {
            if (order != null)
            {
                for (int i = 0; i < order.Length; i++)
                {
                    int currentListId = int.Parse(order[i]);
                    TaskList list = db.TaskLists.Where(x => x.Id == currentListId).FirstOrDefault(); ;
                    list.Position = i;
                    db.Entry(list).State = EntityState.Modified;
                }
                db.SaveChanges();
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
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
