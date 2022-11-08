using McqsUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace McqsUI.Services
{
    public class QuizServiceStub
    {
        private List<QuizDTO> quizzes = new();

        public QuizServiceStub()
        {
            Init();
        }

        private void Init()
        {
            quizzes.Add(new QuizDTO
            {
                Id = 1,
                Name = "Stub Quiz No.1",
                Description = "from Stub Service",
                Duration = new TimeSpan(0, 20, 0),
                Subject = Constants.SubjectEnum.All,
                Questions = CreateQuestions()
            });

            quizzes.Add(new QuizDTO
            {
                Id = 2,
                Name = "Stub Quiz No.2",
                Description = "from Stub Service",
                Duration = new TimeSpan(0, 40, 0),
                Subject = Constants.SubjectEnum.Science,
                Questions = CreateQuestions()
            });
        }

        private List<QuestionDTO> CreateQuestions()
        {
            var questions = new List<QuestionDTO>();

            for (int i = 0; i < 10; i++)
            {
                questions.Add(new QuestionDTO
                {
                    Id = (i + 1),
                    Name = $"i am question no.{i + 1}",
                    Description = $"i am description no.{i + 1}",
                    OptionA = "i am option-A",
                    OptionB = "i am option-B",
                    OptionC = "i am option-C",
                    OptionD = "i am option-D",
                });
            }

            return questions;
        }

        public async Task<QuizDTO> GetQuizAsync(long Id)
        {
            var quiz = quizzes.FirstOrDefault(q => q.Id == Id);            

            return await Task.FromResult<QuizDTO>(quiz);
        }

        public async Task<QuizResultDTO> AnalyzeQuizAsync(QuizDTO quiz)
        {
            QuizResultDTO returnValue = new();

            returnValue.Id = quiz.Id;
            returnValue.Name = quiz.Name;
            returnValue.Description = quiz.Description;
            returnValue.Answers = new List<SolvedQuestionDTO>();

            for (int i = 0; i < quiz.Questions.Count; i++)
            {
                returnValue.Answers.Add(new(quiz.Questions[i]));
                returnValue.Answers[i].Marks = 5;
                if (i % 2 == 0)
                {
                    returnValue.Answers[i].Marks = 0;
                }
            }

            return await Task.FromResult<QuizResultDTO>(returnValue);
        }
    }
}
