using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PaiTwitterApi.Models
{
    public class TLike
    {
        [Key]
        public int LikeId { get; set; }
        public int CreatorId { get; set; }
        public int PostId { get; set; }
        public int CommentId { get; set; }
        public DateTime CreatedDate { get; set; }

        [ForeignKey("CreatorId")]
        public TUser Creator;

        [ForeignKey("PostId")]
        public TPost Post;

        [ForeignKey("CommentId")]
        public TComment Comment;
    }
}
