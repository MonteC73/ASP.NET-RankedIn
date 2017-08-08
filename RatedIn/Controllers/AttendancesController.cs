using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RatedIn.Models;

namespace RatedIn.Controllers
{
    public class AttendancesController : Controller
    {
        private ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public ActionResult Attend(int tournamentId, int playerId)
        {
            var attendance = new Attendance()
            {
                PlayerId = playerId,
                TournamentId = tournamentId
            };

            _context.Attendances.Add(attendance);
            _context.SaveChanges();

            return RedirectToAction("Index", "Tournament");
        }
    }
}