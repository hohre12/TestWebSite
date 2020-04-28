using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebSIte.Models;

namespace TestWebSIte.ViewModel
{
    public class AnswerViewModel
    {
        public List<Inquire> Inquires { get; set; }
        //public SelectList Categories { get; set; }
        public List<User> Users { get; set; }
        public List<int> UserNo { get; set; }
        
        //public List<string> Titles { get; set; }
    }
}
