using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebSIte.ViewModel
{
    public class ChkPwViewModel
    {
        // ID 찾을때
        [Required]
        public string UserPw { get; set; }
    }
}
