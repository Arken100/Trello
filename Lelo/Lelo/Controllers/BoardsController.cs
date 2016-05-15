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
using Microsoft.AspNet.Identity;

namespace Lelo.Controllers
{
    public class BoardsController : BaseController
    {
        public BoardsController()
        {
            //CurrentUserId = User != null ? new Guid(User.Identity.GetUserId()) : Guid.Empty;
            //CurrentUser = CurrentUserId != Guid.Empty ? db.Users.First(x => x.Id == CurrentUserId) : null;
        }

        // GET: Boards
        [Authorize(Roles ="Admin, LeloUser")]
        public ActionResult Index()
        {
            SetCurrentUser();

            var boards = db.Boards.Include(b => b.Team).Include(b => b.User);


            //Jeżeli nie Admin - filtruj po user    
            if (User.IsInRole("LeloUser"))
            {
                var uid = GetCurrentUserId();
                boards = boards.Where(x => x.UserId == uid || x.Team.Users.Select(xx=> xx.Id).Contains(CurrentUserId));
                return View(boards.ToList());
            }

            return View(boards.ToList());
        }

        // GET: Boards/Details/5
        [Authorize(Roles = "Admin, LeloUser")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Board board = db.Boards.Find(id);
            if (board == null)
            {
                return HttpNotFound();
            }
            return View(board);
        }

        // GET: Boards/Create
        [Authorize(Roles = "Admin, LeloUser")]
        public ActionResult Create()
        {
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Boards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, LeloUser")]
        public ActionResult Create([Bind(Include = "Id,Title,Description,UserId,TeamId")] Board board)
        {
            if (ModelState.IsValid)
            {
                if (board.UserId == Guid.Empty)
                {
                    board.UserId = GetCurrentUserId();
                }

                if (User.IsInRole("LeloUser"))
                {
                    SetCurrentUser();

                    Team team = new Team()
                    {
                        Name = board.Title.Replace(" ", "") + " - " +   CurrentUser.UserName,
                        OwnerId = CurrentUserId
                        
                    };

                    board.Team = team;
                }

                db.Boards.Add(board);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", board.TeamId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", board.UserId);
            return View(board);
        }

        // GET: Boards/Edit/5
        [Authorize(Roles = "Admin, LeloUser")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Board board = db.Boards.Find(id);
            if (board == null)
            {
                return HttpNotFound();
            }
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", board.TeamId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", board.UserId);
            return View(board);
        }

        // POST: Boards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, LeloUser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,UserId,TeamId")] Board board)
        {
            if (ModelState.IsValid)
            {
                db.Entry(board).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", board.TeamId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", board.UserId);
            return View(board);
        }

        // GET: Boards/Delete/5
        [Authorize(Roles = "Admin, LeloUser")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Board board = db.Boards.Find(id);
            if (board == null)
            {
                return HttpNotFound();
            }
            return View(board);
        }

        // POST: Boards/Delete/5
        [Authorize(Roles = "Admin, LeloUser")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Board board = db.Boards.Find(id);
            db.Boards.Remove(board);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult EditTask(int? id)
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

            ViewBag.BoardId = db.TaskLists.Find(leloTask.TaskListId).BoardId.Value;


            return View(leloTask);
        }

        // POST: LeloTasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTask([Bind(Include = "Id,Name,Description,TaskListId")] LeloTask leloTask)
        {
            if (ModelState.IsValid)
            {
                db.Entry(leloTask).State = EntityState.Modified;
                db.SaveChanges();

                int boardId = db.TaskLists.Where(x => x.Id == leloTask.TaskListId).FirstOrDefault().BoardId.Value;

                Board board = db.Boards.Where(x => x.Id == boardId).FirstOrDefault();
                return RedirectToAction("Details", new { Id = board.Id });

            }

            return View("Index");
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
