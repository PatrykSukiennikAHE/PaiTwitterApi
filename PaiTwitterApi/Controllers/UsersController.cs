using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PaiTwitterApi.Models;
using PaiTwitterApi.Tools;

namespace PaiTwitterApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly PaiTwitterContext _context;


        public UsersController(PaiTwitterContext context)
        {
            _context = context;
        }

        // GET: Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TUser>>> GetUserInfo()
        {
            TUser user = await _context.TUser.Where(u => u.UserId == User.GetLoggedInUserId<int>()).FirstOrDefaultAsync();
            return Ok(new { 
                    id = user.UserId,
                    firstname = user.FirstName,
                    surname = user.LastName
                    });
        }

        // GET: Users/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TUser>> GetUsers(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var tUser = await _context.TUser
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (tUser == null)
            {
                return NotFound();
            }

            var follow = await _context.TFollow
                    .Where(f => 
                                f.FollowerId == User.GetLoggedInUserId<int>()
                                && f.FollowedId == id
                                ).FirstOrDefaultAsync();

            return Ok(new
            {
                id = tUser.UserId,
                FirstName = tUser.FirstName,
                LastName = tUser.LastName,
                Description = tUser.Description,
                isFollowed = follow == null ? 0 : 1,
                isSelf = tUser.UserId == User.GetLoggedInUserId<int>() ? 1 : 0
            });
        }

        // POST: Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,FirstName,LastName,UserName,Email,Password,CreatedDate,LastActivity")] TUser tUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tUser);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> EditUser(int id, TUser TUser)
        {
            if (id != TUser.UserId)
            {
                return BadRequest();
            }

            _context.Entry(TUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TUserExists(id))
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


        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tUser = await _context.TUser
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (tUser == null)
            {
                return NotFound();
            }

            _context.TUser.Remove(tUser);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool TUserExists(int id)
        {
            return _context.TUser.Any(e => e.UserId == id);
        }
    }
}
