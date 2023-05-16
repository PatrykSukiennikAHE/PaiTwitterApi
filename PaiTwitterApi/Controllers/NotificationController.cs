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
    public class NotificationController : Controller
    {
        private readonly PaiTwitterContext _context;

        public NotificationController(PaiTwitterContext context)
        {
            _context = context;
        }

        [HttpGet("api/notifications/new")]
        public async Task<ActionResult<IEnumerable<TPost>>> GetNewNotifications()
        {
            var notifications = await _context.TNotification.Where(n => n.UserId == User.GetLoggedInUserId<int>() && n.ReadDate == null).ToListAsync();
            return Ok(notifications);
        }

        [HttpGet("api/notifications/all")]
        public async Task<ActionResult<IEnumerable<TPost>>> GetAllNotifications()
        {
            var notifications = await _context.TNotification.Where(n => n.UserId == User.GetLoggedInUserId<int>()).ToListAsync();
            return Ok(notifications);
        }


        [HttpPost("api/notifications/readAll")]
        public async Task<ActionResult<IEnumerable<TPost>>> ReadNotifications()
        {
            var notifications = await _context.TNotification.Where(n => n.UserId == User.GetLoggedInUserId<int>() && n.ReadDate == null).ToListAsync();
            notifications.ForEach(n => n.ReadDate = DateTime.Now);
            await _context.SaveChangesAsync();
            return Ok(notifications);
        }
    }
}
