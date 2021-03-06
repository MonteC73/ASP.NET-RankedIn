﻿using RatedIn.Models;
using RatedIn.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace RatedIn.Controllers
{
    public class TournamentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TournamentController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Details
        public ActionResult Index()
        {
            return View(_context.Tournaments.ToList());
        }

        public ActionResult Details(int? id)
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

            var attendaces = _context.Attendances.Where(a => a.TournamentId == id);
            var players = _context.Attendances.Where(a => a.TournamentId == id)
                .Select(a => a.Player)
                .ToList();

            var tournamentView = new TournamentViewModel
            {
                TournamentId = tournament.Id,
                TournamentAdminId = tournament.AdminId,
                StartDate = tournament.StartDate,
                EndDate = tournament.EndDate,
                TournamentName = tournament.Name,
                Attendances = attendaces,
                Players = players
            };
            return View(tournamentView);
        }

        //// GET: Player/Create
        //public ActionResult AddPlayer(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var tournament = _context.Tournaments.Find(id);
        //    if (tournament == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tournament);
        //}

        //// POST: Player/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //public ActionResult AddPlayer([Bind(Include = "Id,Player")] Details tournament)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var currentPlayers = tournament.Player.ToList();
        //        var players = _context.Player.ToList();
        //        var answer = players.Except(currentPlayers);

        //        return RedirectToAction("Index");
        //    }

        //    return View(tournament);
        //}

        //[HttpPost]
        //public ActionResult AddPlayer([Bind(Include = "Id,Player")] Details tournament)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var currentPlayers = tournament.Player.ToList();
        //        var players = _context.Player.ToList();
        //        var answer = players.Except(currentPlayers);

        //        return RedirectToAction("Index");
        //    }

        //    return View(tournament);
        //}

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

            var players = _context.Attendances.Where(a => a.TournamentId == id)
                                  .Select(p => p.Player)
                                  .Include(f => f.FilePaths)
                                  .OrderBy(p => Guid.NewGuid()).Take(10)
                                  .OrderBy(p => p.Games).Take(2)
                                  .ToList();


            var gameView = new GameViewModel
            {
                TournamentId = tournament.Id,
                Tournament = tournament,
                Players = players
            };

            return View(gameView);
        }

        public ActionResult UpdateRank(int won, int lost, int tournamentId)
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

            return RedirectToAction("Game", new {id = tournamentId});
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