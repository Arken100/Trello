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
    public class BoardsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private Guid CurrentUserId;
        private ApplicationUser CurrentUser;


        public BoardsController()
        {

            CurrentUserId = User != null ? new Guid(User.Identity.GetUserId()) : Guid.Empty;
            CurrentUser = CurrentUserId != Guid.Empty ? db.Users.First(x => x.Id == CurrentUserId) : null;
        }

        public Guid GetCurrentUserId()
        {
            var toReturn = User != null ? new Guid(User.Identity.GetUserId()) : Guid.Empty;
            return toReturn;
        }

        public void SetCurrentUser()
        {
            CurrentUserId = User != null ? new Guid(User.Identity.GetUserId()) : Guid.Empty;
            CurrentUser = db.Users.First(x => x.Id == CurrentUserId);
        }





        // GET: Boards
        public ActionResult Index()
        {
            SetCurrentUser();

            var boards = db.Boards.Include(b => b.Team).Include(b => b.User);


            //Jeżeli nie Admin - filtruj po user    
            if (!User.IsInRole("Admin"))
            {
                var uid = GetCurrentUserId();
                boards = boards.Where(x => x.UserId == uid || x.Team.Users.Select(xx=> xx.Id).Contains(CurrentUserId));
                return View(boards.ToList());
            }

            return View(boards.ToList());
        }

        // GET: Boards/Details/5
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
        public ActionResult Create([Bind(Include = "Id,Title,Description,UserId,TeamId")] Board board)
        {
            if (ModelState.IsValid)
            {
                db.Boards.Add(board);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", board.TeamId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", board.UserId);
            return View(board);
        }

        // GET: Boards/Edit/5
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Board board = db.Boards.Find(id);
            db.Boards.Remove(board);
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
