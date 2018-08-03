using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using GigHub.Models;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index(string query)
        {
            var upcomingGigs = _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);
            if (!string.IsNullOrWhiteSpace(query))
            {
                upcomingGigs = upcomingGigs.Where(g =>
                      g.Artist.UserName.Contains(query) ||
                      g.Genre.Name.Contains(query) ||
                      g.Venue.Contains(query));
            }
            return View("Gigs", upcomingGigs);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}