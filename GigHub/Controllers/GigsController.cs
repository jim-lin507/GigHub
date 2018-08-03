using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public ActionResult Search(string searchItem)
        {
            return RedirectToAction("Index", "Home", new { query = searchItem });
        }
        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Gigs
                .Include(g => g.Genre)
                .Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now
                && !g.IsCanceled).ToList();
            return View(gigs);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);
            var viewModel = new GigFormViewModel
            {
                Id = gig.Id,
                Heading = "Edit a Gig",
                Genres = _context.Genres.ToList(),
                Date = gig.DateTime.ToString("d MMM yyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                GenreId = gig.GenreId,
                Venue = gig.Venue
            };
            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.Id == viewModel.Id && g.ArtistId == userId);

            gig.DateTime = viewModel.GetDateTime();
            gig.GenreId = viewModel.GenreId;
            gig.Venue = viewModel.Venue;

            _context.SaveChanges();
            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel()
            {
                Heading = "Create a Gig",
                Genres = _context.Genres.ToList()
            };
            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }
            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.GenreId,
                Venue = viewModel.Venue
            };
            _context.Gigs.Add(gig);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}