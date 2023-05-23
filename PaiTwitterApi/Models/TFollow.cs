using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PaiTwitterApi.Models
{
    public class TFollow
    {
        [Key]
        public int FollowId { get; set; }
        public int FollowedId { get; set; }
        public int FollowerId { get; set; }
        public DateTime CreatedDate { get; set; }

        [ForeignKey("FollowedId")]
        public virtual TUser Followed { get; set; }

        [ForeignKey("FollowerId")]
        public virtual TUser Follower { get; set; }
    }
}
