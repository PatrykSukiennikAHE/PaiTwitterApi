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
                            x = (DateTime.Now.Date - p.CreatedDate.Date).Days,
                            CreatedDate = 
                                (DateTime.Now.Date - p.CreatedDate.Date).Days == 0 ? p.CreatedDate.ToString("HH:mm") :
                                (DateTime.Now.Date - p.CreatedDate.Date).Days == 1 ? "wczoraj " + p.CreatedDate.ToString("HH:mm") :
                                (DateTime.Now.Date - p.CreatedDate.Date).Days == 2 ? "przedwczoraj " + p.CreatedDate.ToString("HH:mm") :
                                    p.CreatedDate.ToString("HH:mm:ss MM/dd/yyyy"),

                            Creator = p.Creator == null ? null : p.Creator.FirstName + " " + p.Creator.LastName,
                            CreatorUserName = p.Creator == null ? null : " @" + p.Creator.UserName,
                            CreatorId = p.CreatorId,
                            ContentText = p.ContentText
                        })
                        .OrderByDescending(p => p.CreatedDate)
                        .Take(20)
                        .ToList());
        }

        [HttpGet("api/post/{userId}")]
        public async Task<ActionResult<TPost>> GetPost(int userId)
        {
            var minimumDate = DateTime.Now.AddDays(-14);

            return Ok(_context.TPost
                        .Include(p => p.Creator)
                        .AsEnumerable()
                        .Where(p => p.CreatedDate >= minimumDate && p.CreatorId == userId)
                        .Select(p => new
                        {
                            PostId = p.PostId,
                            x = (DateTime.Now.Date - p.CreatedDate.Date).Days,
                            CreatedDate =
                                (DateTime.Now.Date - p.CreatedDate.Date).Days == 0 ? p.CreatedDate.ToString("HH:mm") :
                                (DateTime.Now.Date - p.CreatedDate.Date).Days == 1 ? "wczoraj " + p.CreatedDate.ToString("HH:mm") :
                                (DateTime.Now.Date - p.CreatedDate.Date).Days == 2 ? "przedwczoraj " + p.CreatedDate.ToString("HH:mm") :
                                    p.CreatedDate.ToString("HH:mm:ss MM/dd/yyyy"),

                            Creator = p.Creator == null ? null : p.Creator.FirstName + " " + p.Creator.LastName,
                            CreatorId = p.CreatorId,
                            ContentText = p.ContentText
                        })
                        .OrderByDescending(p => p.CreatedDate)
                        .Take(20)
                        .ToList());
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
