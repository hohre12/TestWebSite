using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebSIte.Models;

namespace TestWebSIte.ViewModel
{
    public class InquireViewModel
    {
        public List<Inquire> Questions { get; set; }
        public SelectList Categories { get; set; }
        public string CategoryChoice { get; set; }
        // public int FaqNo { get; set; }
        public List<string> Titles { get; set; }

    }
}
