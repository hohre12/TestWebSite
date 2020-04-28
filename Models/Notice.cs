using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebSIte.Models
{
    public class Notice
    {
        [Key]
        public int NoticeNo { get; set; }
        public string NoticeTitle { get; set; }
        public string NoticeContent { get; set; }
        public int UserNo { get; set; }
        public string Regdate { get; set; }
        public int ViewCnt { get; set; }


    }
}
