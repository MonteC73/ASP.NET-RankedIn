using System.Linq;
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


    }
}

/*
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

*/