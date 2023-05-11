using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaiTwitterApi.Models
{
    public class TFollow
    {
        [Key]
        public int FollowId { get; set; }
        public TUser FollowedId { get; set; }
        public TUser FollowerId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
