using RatedIn.DAL;
using RatedIn.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RatedIn.Controllers
{
    public class PlayersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlayersController()
        {
            _context = new ApplicationDbContext();
        }


        // GET: Players
        public ActionResult Index()
        {
            return View(_context.Players.ToList());
        }

        // GET: Players/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Players player = _context.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // GET: Players/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Rating,Games,FilePaths")] Players player, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var photo = new FilePath()
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = FileType.Img,
                    };
                    player.FilePaths = new List<FilePath> {photo};
                    upload.SaveAs(Path.Combine(Server.MapPath("~/files"), photo.FileName));
                }
                _context.Players.Add(player);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(player);
        }

        // GET: Players/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var player = _context.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Rating,Games,FilePaths")] Players player, HttpPostedFileBase upload)
        {
            var entityKey = _context.Players.First(p => p.Id == player.Id);
            Debug.WriteLine("Edit");
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    Debug.WriteLine("Valid");
                    var photo = new FilePath()
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = FileType.Img,
                    };
                    player.FilePaths = new List<FilePath> { photo };
                    upload.SaveAs(Path.Combine(Server.MapPath("~/files"), player.FilePaths.First().FileName));
                }
                Debug.WriteLine("Out {0}: {1}", player.Name, player.FilePaths.First().FileName);

                _context.Entry(_context.Set<Players>().Find(entityKey)).CurrentValues.SetValues(player);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(player);
        }

        // GET: Players/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var player = _context.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        public ActionResult Game()
        {
            var players = _context.Players.OrderBy(p => Guid.NewGuid()).Take(15)
                                    .OrderBy(p => p.Games).Take(2);

            return View(players.ToList());
        }

        public ActionResult UpdateRank(int won, int lost)
        {
            var winner = _context.Players.First(w => w.Id == won);
            var looser = _context.Players.First(w => w.Id == lost);

            int KA = GetFactorK(winner.Rating);
            int KB = GetFactorK(looser.Rating);

            double QA = Math.Pow(10, (double)winner.Rating / 400);
            double QB = Math.Pow(10, (double)looser.Rating / 400);

            double EA = QA / (QA + QB);
            double EB = QB / (QA + QB);

            int RA = (int)Math.Round(winner.Rating + KA * (1 - EA));
            int RB = (int)Math.Round(looser.Rating + KB * (0 - EB));

            winner.Rating = RA;
            looser.Rating = RB;
            winner.Games += 1;
            looser.Games += 1;
            _context.SaveChanges();

            return RedirectToAction("Game");
        }

        protected int GetFactorK(int elo)
        {
            if (elo > 2400)
                return 16;
            if (elo > 2100)
                return 24;
            return 32;
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Players player = _context.Players.First(p => p.Id == id);
            _context.Players.Remove(player);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}
