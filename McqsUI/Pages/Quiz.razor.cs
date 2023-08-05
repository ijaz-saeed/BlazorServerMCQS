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
        private int selectedId;
        private int index;
        private QuizDTO quizDTO = new();
        private QuestionDTO questionDTO = new();

        protected override void OnInitialized()
        {
            selectedId = 0;

            index = 0;
            started = false;
        }

        protected void StartQuiz()
        {
            index = 0;
            quizDTO.Questions.ForEach(a => a.Answer = null);
            questionDTO = quizDTO.Questions[index];
            StateContainer.quizDTO = null;
            RandomSort();

            if (!started)
            {
                started = true;
                questionDTO = quizDTO.Questions[index];
            }
            else
            {
                started = false;
                selectedId = 0;
            }
            myTimer?.StartTimer();
        }

        private void RandomSort() 
        {
            if (DateTime.Now.Minute % 2 == 0)
            {
                quizDTO.Questions.Sort((a, b) => a.Name.CompareTo(b.Name));
            }
            else
            {
                quizDTO.Questions.Sort((a, b) => a.Id.CompareTo(b.Id));
            }
        }

        public async void OnQuizUpdated(ChangeEventArgs e)
        {
            selectedId = Convert.ToInt32(e.Value);
            if (selectedId > 0)
            {
                quizDTO = await Http.GetQuizAsync(selectedId);
                RandomSort();

                index = 0;
                started = false;
            }
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
