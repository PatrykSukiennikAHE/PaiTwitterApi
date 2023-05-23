using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaiTwitterApi.Models;
using PaiTwitterApi.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class FollowDto
{
    public int FollowedId { get; set; }
    public string FollowedName { get; set; }
}

namespace PaiTwitterApi.Controllers
{
    [Authorize]
    [ApiController]
    public class FollowController : Controller
    {
        private readonly PaiTwitterContext _context;

        public FollowController(PaiTwitterContext context)
        {
            _context = context;
        }

        // GET: api/follows
        [HttpGet]
        [Route("api/follow/list")]
        public async Task<ActionResult> GetFollow()
        {
            return Ok(_context.TFollow
                            .Include(f=>f.Followed)
                            .Where(f => f.FollowerId == User.GetLoggedInUserId<int>())
                            .AsEnumerable()
                            .Select(f => new 
                            {
                                FollowedId = f.FollowedId,
                                FollowedName = f.Followed == null ? null : f.Followed.FirstName + " " + f.Followed.LastName
                            })
                            .ToList());
        }

        // POST: follows
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("api/follow/{id}")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Follow(int id)
        {
            int followerId = User.GetLoggedInUserId<int>();
            var follower = await _context.TUser.FirstOrDefaultAsync(u => u.UserId == followerId);
            var followed = await _context.TUser.FirstOrDefaultAsync(u => u.UserId == id);

            if (follower == null || followed == null)
            {
                return NotFound("Nie znaleziono usera");
            }

            if (follower.UserId == followed.UserId)
            {
                return BadRequest("Nie można followować samego siebie!");
            }

            var existingFollow = await _context.TFollow.FirstOrDefaultAsync(f => f.FollowerId == followerId && f.FollowedId == id);
            if (existingFollow != null)
            {
                return BadRequest("Istnieje już taki follow!");
            }

            TFollow follow = new TFollow();
            follow.FollowerId = followerId;
            follow.FollowedId = id;
            _context.Add(follow);
            await _context.SaveChangesAsync();
            return Ok("Dodano follow");
        }

        // POST: follows
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("api/unfollow/{id}")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Unfollow(int id)
        {
            int followerId = User.GetLoggedInUserId<int>();
            var follower = await _context.TUser.FirstOrDefaultAsync(u => u.UserId == followerId);
            var followed = await _context.TUser.FirstOrDefaultAsync(u => u.UserId == id);

            if (follower == null || followed == null)
            {
                return NotFound("Nie znaleziono usera");
            }

            if (follower.UserId == followed.UserId)
            {
                return BadRequest("Nie można follować samego siebie!");
            }

            var existingFollow = await _context.TFollow.FirstOrDefaultAsync(f => f.FollowerId == followerId && f.FollowedId == id);
            if (existingFollow == null)
            {
                return NotFound("Nie znaleziono takiego followa");
            }

            _context.Remove(existingFollow);
            await _context.SaveChangesAsync();
            return Ok("Usunięto followa");
        }
    }
}
