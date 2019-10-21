using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRS_ServerV2.Models;

namespace PRS_ServerV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly PRSDbContext _context;

        public RequestsController(PRSDbContext context)
        {
            _context = context;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\\
        //                                                           ASSIGN REQUEST STATUS                                                ||
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//

        public static string ReqNew = "NEW";
        public static string ReqEdt = "EDIT";
        public static string ReqRev = "REVIEW";
        public static string ReqApp = "APPROVED";
        public static string ReqDen = "DENIED";

        //[HttpPut("{status}/{id}")]
        public async Task<ActionResult<Requests>> SetStatus(string status, int id) {
            var request = await _context.Requests.FindAsync(id);
            if (request == null) {
                return NotFound();
            }
            request.Status = status;
            Recalc(id);
            return Ok(); // says that it worked
        }
 

        [HttpPut("approve/{id}")]
        public async Task<ActionResult<Requests>> SetStatusApprove(int id) {            
            return await SetStatus(ReqApp, id);
        }
        [HttpPut("deny/{id}")]
        public async Task<ActionResult<Requests>> SetStatusDeny(int id) {
            var request = await _context.Requests.FindAsync(id);
            if(request.RejectionReason == null) {
                throw new Exception("Must provide rejection reason"); // this code is untested
            }
            return await SetStatus(ReqDen, id);
        }
        [HttpPut("review/{id}")]
        public async Task<ActionResult<Requests>> SetStatusReview(int id) {
            return await SetStatus(ReqRev, id);
        }
        [HttpPut("edit/{id}")]
        public async Task<ActionResult<Requests>> SetStatusEdit(int id) {
            return await SetStatus(ReqEdt, id);
        }
        [HttpPut("new/{id}")]
        public async Task<ActionResult<Requests>> SetStatusNew(int id) {            
            return await SetStatus(ReqNew, id);
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\\
        //                                                           CUSTOM METHODS                                                       ||
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//

        public async void Recalc(int id) {
            var request = _context.Requests.Find(id);
            if (request == null) { throw new Exception("Request Id not found"); }
            request.Total = _context.RequestLines.Where(rl => rl.Id == id).Sum(rl => rl.Product.Price * rl.Quantity);
            if (request.Total < 50) {
                await SetStatusApprove(request.Id); // this code has not been tested
            }
            _context.SaveChanges();
        }

        // shows all requests with new|review|edit status, excluding requests from current user
        // GET: api/Requests/inreview/{id}
        [HttpGet("inreview/{id}")]
        public async Task<ActionResult<IEnumerable<Requests>>> GetRequestsInReview(int id) {            
            return await _context.Requests.Where(r => r.UserId != id 
                                                                && r.Status != "APPROVED" 
                                                                && r.Status != "DENIED").ToListAsync();
        }

        //// POST: api/Requests/{username}/{password}
        //[HttpPost("Requests/{username}/{password}")]
        //public async Task<ActionResult<Requests>> PostRequests(Requests requests, string username, string password) {
        //    var user = await _context.Users.SingleOrDefaultAsync(e => e.Username.Equals(username) && e.Password.Equals(password));
        //    if (user == null) {
        //        return NotFound();
        //    }
        //    requests.UserId = user.Id;
        //    _context.Requests.Add(requests);
        //    await _context.SaveChangesAsync();
        //    Recalc(requests.Id);

        //    return CreatedAtAction("GetRequests", new { id = requests.Id }, requests);
        //}


        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\\
        //                                                           DEFAULT METHODS                                                      ||
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//


        //// GET: api/Requests
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Requests>>> GetRequests()
        //{            
        //    return await _context.Requests.ToListAsync();
        //}

        // GET: api/Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Requests>>> GetRequests() {
            var requests = await _context.Requests.ToListAsync();
            foreach(var request in requests) {
                Recalc(request.Id);
            }
            _context.SaveChanges();
            return requests;
        }

        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Requests>> GetRequests(int id)
        {
            var requests = await _context.Requests.FindAsync(id);
            Recalc(id);

            if (requests == null)
            {
                return NotFound();
            }
            return requests;
        }

        // PUT: api/Requests/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequests(int id, Requests requests)
        {
            if (id != requests.Id)
            {
                return BadRequest();
            }

            _context.Entry(requests).State = EntityState.Modified;

            try
            {                
                await _context.SaveChangesAsync();
                Recalc(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        //// POST: api/Requests
        //[HttpPost]
        //public async Task<ActionResult<Requests>> PostRequests(Requests requests)
        //{
        //    _context.Requests.Add(requests);
        //    await _context.SaveChangesAsync();
        //    Recalc(requests.Id);
            
        //    return CreatedAtAction("GetRequests", new { id = requests.Id }, requests);
        //}

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Requests>> DeleteRequests(int id)
        {
            var requests = await _context.Requests.FindAsync(id);
            if (requests == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(requests);
            await _context.SaveChangesAsync();
            Recalc(id);

            return requests;
        }

        private bool RequestsExists(int id)
        {            
            return _context.Requests.Any(e => e.Id == id);
        }
    }
}
