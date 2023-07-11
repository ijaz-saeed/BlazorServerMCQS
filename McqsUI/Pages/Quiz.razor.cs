using McqsUI.Models;
using McqsUI.Shared;
using McqsUI.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace McqsUI.Pages
{
    public partial class Quiz : ComponentBase
    {
        private BackTimer? myTimer;
        private bool started;
        private int index;
        private QuizDTO quizDTO = new();
        private QuestionDTO questionDTO = new();
        private string myPath = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            quizDTO = await Http.GetQuizAsync(12);
            index = 0;
            started = false;
            myPath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Data");
        }

        protected void StartQuiz()
        {
            //JsonFileUtils.PrettyWrite(quizDTO, myPath + "/sample.json");

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
            myTimer?.StartTimer();
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

        protected async void OnTimeUp() 
        {
            await JsRuntime.InvokeVoidAsync("alert", "Your time is Up...");

            StateContainer.quizDTO = quizDTO;
            NavigationManager.NavigateTo("quiz-result");
        }
    }
}
