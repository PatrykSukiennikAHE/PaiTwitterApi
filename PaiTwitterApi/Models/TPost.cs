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
        public int? SharedPostId { get; set; }
        [StringLength(100)]
        public string ContentText { get; set; }
        public DateTime CreatedDate { get; set; }

        [ForeignKey("CreatorId")]
        public virtual TUser Creator { get; set; }

        [ForeignKey("SharedPostId")]
        public virtual TPost SharedPost { get; set; }


    }
}
