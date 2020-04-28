using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebSIte.Models
{
    public class Answer
    {
        [Key]
        public int AnswerNo { get; set; }
        public int InquireNo { get; set; }
        // 문의 제목
        public string Title { get; set; }
        // 문의 내용
        public string Questions { get; set; }
        // 선택된 카테고리
        public string CategoryChoice { get; set; }
        // 유저 번호
        public int UserNo { get; set; }
        // 질문한 년도
        public string QuestionYear { get; set; }
        // 질문한 달
        public string QuestionMonth { get; set; }
        // 질문한 날짜
        public string QuestionDay { get; set; }
        // 질문한 시간
        public string QuestionTime { get; set; }

        // 답변 내용
        public string AnswerContent { get; set; }
        // 답변 날짜
        public string AnswerDay { get; set; }


    }
}
