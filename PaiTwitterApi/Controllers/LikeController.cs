using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaiTwitterApi.Models;
using PaiTwitterApi.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaiTwitterApi.Controllers
{
    [Authorize]
    [ApiController]
    public class LikeController : Controller
    {
        private readonly PaiTwitterContext _context;

        public LikeController(PaiTwitterContext context)
        {
            _context = context;
        }

        [HttpGet("api/likes/{postId}")]
        public async Task<ActionResult<IEnumerable<TLike>>> GetLike(int postId)
        {
            var likes = await _context.TLike.Where(l => l.PostId == postId).ToListAsync();
            return Ok(likes);
        }

        [HttpPost("api/like/{postId}")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Like(int postId)
        {
            int usrId = User.GetLoggedInUserId<int>();
            var usr = await _context.TUser.FirstOrDefaultAsync(u => u.UserId == usrId);
            var post = await _context.TPost.FirstOrDefaultAsync(p => p.PostId == postId);

            if (usr == null || post == null)
            {
                return NotFound("Nie znaleziono posta");
            }

            var existingLike = await _context.TLike.FirstOrDefaultAsync(l => l.CreatorId == usrId && l.PostId == postId);
            if (existingLike != null)
            {
                return NotFound("Istnieje już taki like");
            }

            TLike like = new TLike();
            like.PostId = postId;
            like.CreatorId = usrId;
            like.CreatedDate = DateTime.Now;

            _context.Add(like);
            await _context.SaveChangesAsync();
            return Ok("Dodano like");
        }

        [HttpPost("api/unlike/{postId}")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Unlike(int postId)
        {
            int usrId = User.GetLoggedInUserId<int>();
            var usr = await _context.TUser.FirstOrDefaultAsync(u => u.UserId == usrId);
            var post = await _context.TPost.FirstOrDefaultAsync(p => p.PostId == postId);

            if (usr == null || post == null)
            {
                return NotFound("Nie znaleziono posta");
            }

            var existingLike = await _context.TLike.FirstOrDefaultAsync(l => l.CreatorId == usrId && l.PostId == postId);
            if (existingLike == null)
            {
                return NotFound("Nie znaleziono takiego polubienia");
            }

            _context.Remove(existingLike);
            await _context.SaveChangesAsync();
            return Ok("Usunięto followa");
        }
    }
}
