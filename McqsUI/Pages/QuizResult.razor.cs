using McqsUI.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McqsUI.Pages
{
    public partial class QuizResult : ComponentBase
    {
        [Parameter]
        public QuizDTO SolvedQuiz { get; set; }

        public QuizResultDTO ResultDTO { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await Init();
        }

        private async Task Init()
        {
            if (SolvedQuiz == null && StateContainer.quizDTO == null)
            {
                ResultDTO = null;
                return;
            }
            ResultDTO = await Http.AnalyzeQuizAsync(SolvedQuiz == null ? StateContainer.quizDTO : SolvedQuiz);
        }
    }
}
