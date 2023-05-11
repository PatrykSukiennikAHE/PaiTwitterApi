using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PaiTwitterApi.Models
{
    public partial class TUser
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastActivity { get; set; }
    }
}
