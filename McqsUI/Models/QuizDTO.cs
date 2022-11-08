using McqsUI.Constants;
using System;
using System.Collections.Generic;

namespace McqsUI.Models
{
    public class QuizDTO : BaseModel
    {
        public QuizDTO()
        {
            Duration = new TimeSpan(0, 30, 0);
            Subject = SubjectEnum.All;
            Questions = new List<QuestionDTO>();
        }

        public TimeSpan Duration { get; set; }

        public SubjectEnum Subject { get; set; }

        public List<QuestionDTO> Questions { get; set; }
    }
}
