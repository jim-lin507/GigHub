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
    public class AttendanceController : ApiController
    {
        private readonly ApplicationDbContext _context;
        public AttendanceController()
        {
            _context = new ApplicationDbContext();
        }

        public class AttendanceDto
        {
            public int Gigid { get; set; }
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto attend)
        {
            var userId = User.Identity.GetUserId();
            if (_context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == attend.Gigid))
                return BadRequest("The attendance already exists");
            var attendance = new Attendance
            {
                GigId = attend.Gigid,
                AttendeeId = User.Identity.GetUserId()
            };
            _context.Attendances.Add(attendance);
            _context.SaveChanges();
            return Ok();
        }
    }
}
