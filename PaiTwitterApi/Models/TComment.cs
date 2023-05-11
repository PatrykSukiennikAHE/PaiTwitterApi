using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaiTwitterApi.Models
{
    public class TComment
    {
        [Key]
        public int CommentId { get; set; }
        public TUser CreatorId { get; set; }
        public string ContentText { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
