using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaiTwitterApi.Models;
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

        [HttpGet("/post")]
        public async Task<ActionResult<IEnumerable<TPost>>> GetPost()
        {
            var comment = await _context.TPost.ToListAsync();
            return Ok(comment);
        }

        [HttpGet("/post/{id}")]
        public async Task<ActionResult<TPost>> GetPost(int id)
        {
            var post = await _context.TPost.SingleOrDefaultAsync(m => m.PostId == id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [HttpPost("/post")]
        public async Task<ActionResult<TPost>> PostPost(TPost post)
        {
            _context.Entry(post).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPost), new { id = post.PostId }, post);
        }
    }
}
