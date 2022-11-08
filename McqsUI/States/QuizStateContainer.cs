using McqsUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McqsUI.States
{
    public class QuizStateContainer
    {
        private QuizDTO _quiz;

        public QuizDTO quizDTO
        {
            get => _quiz;
            set
            {
                _quiz = value;
                NotifyStateChanged();
            }
        }

        public event Action OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
