using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestWebSIte.Models
{
    public class Board
    {
        // 생성자 만들어 보자 public 
        [Key]
        public int BoardNo { get; set; }

        [Required(ErrorMessage = "제목을 입력하세요.")]
        public string BoardTitle { get; set; }

        [Required(ErrorMessage = "내용을 입력하세요.")]
        public string BoardContent { get; set; }

        //[Display(User.UserName)]
        //[DisplayName(User.UserName)]
        public int UserNo { get; set; }

        public string Regdate { get; set; }

        public string Modidate { get; set; }

        public int ViewCnt { get; set; }

        [ForeignKey("UserNo")]
        public virtual User User { get; set; }
    }
}
