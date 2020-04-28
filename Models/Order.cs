using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebSIte.Models
{
    public class Order
    {
        [Key]
        public int OrderNo { get; set; }
        public int BoardNo { get; set; }
        public int UserNo { get; set; }
        public string BoardTitle { get; set; }
        public string BoardContent { get; set; }
        public string OrderDay { get; set; }
        public string OrderMonth { get; set; }
        public string OrderYear { get; set; }
        
        [ForeignKey("BoardNo")]
        public virtual Board Board { get; set; }
        
    }
}
