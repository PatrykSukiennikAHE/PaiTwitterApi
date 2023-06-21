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
    [ApiController]
    public class UsersController : Controller
    {
        private readonly PaiTwitterContext _context;


        public UsersController(PaiTwitterContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        [Route("api/users")]
        public async Task<ActionResult<IEnumerable<TUser>>> GetUserInfo()
        {
            TUser user = await _context.TUser.Where(u => u.UserId == User.GetLoggedInUserId<int>()).FirstOrDefaultAsync();
            return Ok(new { 
                    id = user.UserId,
                    firstname = user.FirstName,
                    surname = user.LastName
                    });
        }

        [Authorize]
        [Route("api/users/{id}")]
        [HttpGet]
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

        [HttpPost]
        [Route("api/users/register")]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,UserName,Email,Password")] TUser tUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TUser user = await _context.TUser.FirstOrDefaultAsync(u => u.Email == tUser.Email || u.UserName == tUser.UserName);
                    if (user != null)
                    {
                        return BadRequest(new { error = "Konto z taką nazwą lub emailem jest już zajęte" });
                    }

                    tUser.CreatedDate = DateTime.Now;

                    _context.Add(tUser);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                return BadRequest();
            } catch(Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
            
        }

        [Authorize]
        [Route("api/users/{id}")]
        [HttpPut]
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


        [Authorize]
        [Route("api/users/{id}")]
        [HttpDelete]
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

        [Authorize]
        [HttpGet]
        [Route("api/usersearch/{phrase}")]
        public async Task<ActionResult<IEnumerable<TUser>>> GetUserBySearchPhrase(String? phrase)
        {
            return Ok(_context.TUser
                        .AsEnumerable()
                        .Where(u => 
                                u.UserId != User.GetLoggedInUserId<int>()
                                && (u.FirstName.Contains(phrase) || u.LastName.Contains(phrase))
                                )
                        .Select(u => new
                        {
                            UserId = u.UserId,
                            Name = u.FirstName + " " + u.LastName,
                            UserName = u.UserName,
                            LastActivity = u.LastActivity
                        })
                        .OrderByDescending(u => u.LastActivity)
                        .Take(20)
                        .ToList());
        }
    }


}
