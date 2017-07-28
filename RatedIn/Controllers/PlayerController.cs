using RatedIn.DAL;
using RatedIn.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace RatedIn.Controllers
{
    public class PlayerController : Controller
    {
        private RankingContext db = new RankingContext();

        // GET: Player
        public ActionResult Index()
        {
            return View(db.Players.ToList());
        }

        // GET: Player/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // GET: Player/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Player/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,URL,Rating,Games")] Player player)
        {
            if (ModelState.IsValid)
            {
                db.Players.Add(player);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(player);
        }

        // GET: Player/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: Player/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,URL,Rating,Games")] Player player)
        {
            if (ModelState.IsValid)
            {
                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(player);
        }

        // GET: Player/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        public ActionResult Game()
        {
            var players = db.Players.OrderBy(p => Guid.NewGuid()).Take(15)
                                    .OrderBy(p => p.Games).Take(2);

            return View(players.ToList());
        }

        public ActionResult UpdateRank(int won, int lost)
        {
            var winner = db.Players.First(w => w.Id == won);
            var looser = db.Players.First(w => w.Id == lost);

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
            db.SaveChanges();

            return RedirectToAction("Game");
        }

        public int GetFactorK(int elo)
        {
            if (elo > 2400)
                return 16;
            if (elo > 2100)
                return 24;
            return 32;
        }

        // POST: Player/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Player player = db.Players.Find(id);
            db.Players.Remove(player);
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
