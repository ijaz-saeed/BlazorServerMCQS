using McqsUI.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McqsUI.Pages
{
    public partial class QuizResult : ComponentBase
    {
        [Parameter]
        public QuizDTO? SolvedQuiz { get; set; }

        public QuizResultDTO? ResultDTO { get; set; }

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

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && ResultDTO != null)
            {
                var dataPoints = new List<int> { ResultDTO.Answers.Count(a => a.Marks > 0),
                ResultDTO.Answers.Count(a => a.Marks <= 0) };

                await JSRuntime.InvokeVoidAsync("drawChart", dataPoints.ToArray());
            }
        }
    }
}
