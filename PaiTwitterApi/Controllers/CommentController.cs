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
    public class CommentController : Controller
    {
        private readonly PaiTwitterContext _context;

        public CommentController(PaiTwitterContext context)
        {
            _context = context;
        }

        
        [HttpGet("api/comment/{postId}")]
        public async Task<ActionResult<IEnumerable<TComment>>> GetCommentsForPost(int postId)
        {
            var comment = await _context.TComment.Where(c => c.PostId == postId).ToListAsync();
            return Ok(comment);
        }


        [HttpPost("api/comment/{postId}")]
        public async Task<IActionResult> CommentPost(int postId, [Bind("ContentText")] TComment comment)
        {
            int userId = User.GetLoggedInUserId<int>();
            var user = await _context.TUser.FirstOrDefaultAsync(u => u.UserId == userId);
            var post = await _context.TPost.FirstOrDefaultAsync(p => p.PostId == postId);

            if (ModelState.IsValid)
            {
                comment.CreatedDate = DateTime.Now;
                comment.CreatorId = userId;
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else return BadRequest();

            if (user == null || post == null)
            {
                return NotFound("Nie znaleziono usera lub posta");
            }

            
            comment.CreatedDate = DateTime.Now;
            _context.Add(comment);
            await _context.SaveChangesAsync();
            return Ok("Dodano komentarz");
        }

        [HttpDelete("api/comment/{commentId}")]
        public async Task<ActionResult<IEnumerable<TComment>>> DeleteComment(int commentId)
        {
            var comment = await _context.TComment.Where(c => c.CommentId == commentId).FirstOrDefaultAsync();
            if (comment == null)
            {
                return NotFound("Nie znaleziono komentarza");
            }
            else
            {
                _context.Remove(comment);
                return Ok(comment);
            }
        }

    }
}
