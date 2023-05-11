using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaiTwitterApi.Models
{
    public class TLike
    {
        [Key]
        public int LikeId { get; set; }
        public TUser CreatorId { get; set; }
        public TPost PostId{ get; set; }
        public TComment CommentId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
