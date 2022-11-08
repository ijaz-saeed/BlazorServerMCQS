using McqsUI.Models;
using McqsUI.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace McqsUI.Pages
{
    public partial class Quiz : ComponentBase
    {
        private BackTimer myTimer;
        private bool started;
        private int index;
        private QuizDTO quizDTO = new();
        private QuestionDTO questionDTO = new();

        protected override async Task OnInitializedAsync()
        {
            quizDTO = await Http.GetQuizAsync(2);
            index = 0;
            started = false;
        }

        protected void StartQuiz()
        {
            index = 0;
            quizDTO.Questions.ForEach(a => a.Answer = null);
            questionDTO = quizDTO.Questions[index];
            StateContainer.quizDTO = null;

            if (!started)
            {
                started = true;
                questionDTO = quizDTO.Questions[index];
            }
            else
            {
                started = false;
            }
            myTimer.StartTimer();
        }

        protected void SelectAnswer(ChangeEventArgs args)
        {
            if (args.Value != null)
            {
                questionDTO.Answer = args.Value as string;
            }
        }
        protected void Next()
        {
            index++;
            questionDTO = quizDTO.Questions[index];
        }
        protected void Previous()
        {
            index--;
            questionDTO = quizDTO.Questions[index];
        }

        protected async Task Finish()
        {
            bool confirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Are you sure? it will finish the Quiz.");
            if (confirmed)
            {
                StateContainer.quizDTO = quizDTO;
                NavigationManager.NavigateTo("quiz-result");
            }
        }
    }
}
