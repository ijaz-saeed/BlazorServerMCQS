﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McqsUI.Models
{
    public class QuestionDTO : BaseModel
    {
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string? Answer { get; set; }

        public QuestionDTO() : base()
        {
            OptionA = string.Empty;
            OptionB = string.Empty;
            OptionC = string.Empty;
            OptionD = string.Empty;
            Answer = string.Empty;
        }
    }
}
