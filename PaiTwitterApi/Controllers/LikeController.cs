using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaiTwitterApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaiTwitterApi.Controllers
{
    [Route("api/like")]
    [ApiController]
    public class LikeController : Controller
    {
        private readonly PaiTwitterContext _context;

        public LikeController(PaiTwitterContext context)
        {
            _context = context;
        }

        // GET: api/likes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TLike>>> GetLike()
        {
            var comment = await _context.TLike.ToListAsync();
            return Ok(comment);
        }
    }
}
