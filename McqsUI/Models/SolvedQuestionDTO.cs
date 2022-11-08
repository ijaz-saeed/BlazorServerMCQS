using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McqsUI.Models
{
    public class SolvedQuestionDTO : QuestionDTO
    {
        public SolvedQuestionDTO()
        {

        }

        public SolvedQuestionDTO(QuestionDTO question)
        {
            Id = question.Id;
            Name = question.Answer;
            Description = question.Description;

            Answer = question.Answer;
            OptionA = question.OptionA;
            OptionB = question.OptionB;
            OptionC = question.OptionC;
            OptionD = question.OptionD;
        }

        public int Marks { get; set; }
    }
}
