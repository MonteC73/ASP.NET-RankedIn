using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using RatedIn.Models;

namespace RatedIn.Controllers
{
    public class TournamentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TournamentController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Tournament
        public ActionResult Index()
        {
            return View(_context.Tournaments.ToList());
        }

        // GET: Tournaments/5
        public ActionResult Game(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tournament = _context.Tournaments.Find(id);
            if (tournament == null)
            {
                return HttpNotFound();
            }

            var players = tournament.Players.OrderBy(p => Guid.NewGuid()).Take(15)
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

    }
}