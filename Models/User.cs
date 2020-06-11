using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebSIte.Models
{
    public class User
    {
        [Key]
        public int UserNo { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string UserPw { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string UserEmail { get; set; }

        [Required]
        public string UserIntro { get; set; }

        public string SignUpDay { get; set; }
        public string SignUpMonth { get; set; }
        public string SignUpYear { get; set; }
        public byte[] UserSalt { get; set; }

    }
}
