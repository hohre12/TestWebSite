using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebSIte.Models
{
    public class Faq
    {
        [Key]
        public int FaqNo { get; set; }
        public string FaqTItle { get; set; }
        public string FaqAnswer { get; set; }
        // public DateTime Regdate { get; set; } -> 이거 정확한 시간 구분이 필요할때 사용하면 될듯 ( 회원가입 시간같은거? )
        public string Regdate { get; set; }
        public string CategoryChoice { get; set; }
        public int UserNo { get; set; }
        

    }
}
