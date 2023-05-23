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
    public class PostController : Controller
    {
        private readonly PaiTwitterContext _context;

        public PostController(PaiTwitterContext context)
        {
            _context = context;
        }

        [HttpGet("api/post")]
        public async Task<ActionResult<IEnumerable<TPost>>> GetPost()
        {
            var minimumDate = DateTime.Now.AddDays(-14);

            return Ok(_context.TPost
                        .Include(p => p.Creator)
                        .AsEnumerable()
                        .Where(p => p.CreatedDate >= minimumDate)
                        .Select(p => new
                        {
                            PostId = p.PostId,
                            CreatedDate = p.CreatedDate.ToString("HH:mm:ss MM/dd/yyyy"),
                            Creator = p.Creator == null ? null : p.Creator.FirstName + " " + p.Creator.LastName,
                            ContentText = p.ContentText
                        })
                        .ToList());
        }

        [HttpGet("api/post/{id}")]
        public async Task<ActionResult<TPost>> GetPost(int id)
        {
            var post = await _context.TPost.SingleOrDefaultAsync(m => m.PostId == id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [HttpPost("api/post")]
        public async Task<ActionResult<TPost>> PostPost(TPost post)
        {
            post.CreatedDate = DateTime.Now;
            post.CreatorId = User.GetLoggedInUserId<int>();

            _context.Entry(post).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPost), new { id = post.PostId }, post);
        }
    }
}
