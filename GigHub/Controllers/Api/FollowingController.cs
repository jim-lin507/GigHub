using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    
    public class FollowingController : ApiController
    {
        private readonly ApplicationDbContext _context;
        public FollowingController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        [Authorize]
        public IEnumerable<Following> GetFollowedArtists()
        {
            var userId = User.Identity.GetUserId();
            var followedArtists = _context.Followings
                .Where(f => f.FollowerId == userId)
                .ToList();
            return followedArtists;
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult Follow(FollowingDto follow)
        {
            var userId = User.Identity.GetUserId();
            if (_context.Followings.Any(f=>f.FolloweeId==follow.FolloweeId&&f.FollowerId==userId))
                return BadRequest("The following already exists");
            var following = new Following
            {
                FolloweeId = follow.FolloweeId,
                FollowerId = User.Identity.GetUserId()
            };
            _context.Followings.Add(following);
            _context.SaveChanges();
            return Ok();
        }
    }
}
