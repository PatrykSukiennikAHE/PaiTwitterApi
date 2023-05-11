using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaiTwitterApi.Models
{
    public class TPost
    {
        [Key]
        public int PostId { get; set; }
        public TUser CreatorId { get; set; }
        public TPost SharedPostId { get; set; }
        public string ContentText { get; set; }
        public byte[] Image { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
