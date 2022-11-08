using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McqsUI.Models
{
    public class QuizResultDTO : BaseModel
    {
        public List<SolvedQuestionDTO> Answers { get; set; }
    }
}
