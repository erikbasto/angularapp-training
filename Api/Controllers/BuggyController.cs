using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class BuggyController : BaseApiController
    {
        private readonly DataContext _context;

        public BuggyController(DataContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return "secret text";
        }

        [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFound()
        {
            var thing = _context.Users.Find(-1);
            if (thing == null) return NotFound();
            return thing;
        }

        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            try
            {
                var thing = _context.Users.Find(-1);
                var error = thing.ToString();
                return error;
            }
            catch
            {
                return StatusCode(500, "Internal error, please retry!");
            }
         
        }

        [HttpGet("raw-server-error")]
        public ActionResult<string> GetRawServerError()
        {
            var thing = _context.Users.Find(-1);
            var error = thing.ToString();
            return error;
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest ()
        {
            return BadRequest("This was not a good request.");
        }
    }
}

