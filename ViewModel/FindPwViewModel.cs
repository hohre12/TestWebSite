using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebSIte.ViewModel
{
    public class FindPwViewModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string UserEmail { get; set; }
    }
}
