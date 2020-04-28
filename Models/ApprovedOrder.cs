using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebSIte.Models
{
    public class ApprovedOrder
    {
        [Key]
        public int ApprovalNo { get; set; }
        public int OrderNo { get; set; }
        public int BoardNo { get; set; }
        public int UserNo { get; set; }
        public string BoardTitle { get; set; }
        public string BoardContent { get; set; }

        //[ForeignKey("OrderNo")]
        //public virtual Order Order { get; set; }
    }
}
