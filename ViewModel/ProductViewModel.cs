using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebSIte.Models;

namespace TestWebSIte.ViewModel
{
    public class ProductViewModel
    {
        public List<Board> Products { get; set; }
        public SelectList Titles { get; set; }
        public string ProductChoice { get; set; }
        public int BoardNo { get; set; }
        // 안되면 지우기
        public List<string> Contents { get; set; }
    }
}
