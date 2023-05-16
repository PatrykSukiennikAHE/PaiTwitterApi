using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PaiTwitterApi.Models
{
    public class TNotification
    {
        [Key]
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime ReadDate { get; set; }
        public DateTime CreatedDate { get; set; }

        [ForeignKey("UserId")]
        public TUser User;
    }
}
