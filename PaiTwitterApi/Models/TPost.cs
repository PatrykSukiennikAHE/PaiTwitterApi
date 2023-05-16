using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PaiTwitterApi.Models
{
    public class TPost
    {
        [Key]
        public int PostId { get; set; }
        public int CreatorId { get; set; }
        public int SharedPostId { get; set; }
        public string ContentText { get; set; }
        public byte[] Image { get; set; }
        public DateTime CreatedDate { get; set; }

        [ForeignKey("CreatorId")]
        public TUser Creator;

        [ForeignKey("SharedPostId")]
        public TPost SharedPost;


    }
}
