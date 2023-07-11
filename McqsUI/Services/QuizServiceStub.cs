using McqsUI.Models;
using McqsUI.Utils;
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
        private string _filePath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Data") + "/sample.json";

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
                Duration = new TimeSpan(0, 30, 0),
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
            var quiz = new QuizDTO();

            if (Id <= 10)
            {
                quiz = quizzes.FirstOrDefault(q => q.Id == Id);
            }
            else
            {
                var qList = JsonFileUtils.ReadAllData(_filePath);
                quiz = qList.FirstOrDefault(q => q.Id == Id);
                foreach (var q in quiz.Questions)
                {
                    q.Answer = string.Empty;
                }
            }

            return await Task.FromResult<QuizDTO>(quiz);
        }

        public async Task<QuizResultDTO> AnalyzeQuizAsync(QuizDTO quiz)
        {
            QuizResultDTO returnValue = new();

            returnValue.Id = quiz.Id;
            returnValue.Name = quiz.Name;
            returnValue.Description = quiz.Description;
            returnValue.Answers = new List<SolvedQuestionDTO>();

            if (quiz.Id <= 10)
            {
                for (int i = 0; i < quiz.Questions.Count; i++)
                {
                    returnValue.Answers.Add(new(quiz.Questions[i]));
                    returnValue.Answers[i].Marks = 5;
                    if (i % 2 == 0)
                    {
                        returnValue.Answers[i].Marks = 0;
                    }
                }
            }
            else 
            {
                var serverQuiz = JsonFileUtils.GetQuizbyId(_filePath, quiz.Id);
                foreach (var q in quiz.Questions) 
                {
                    var serverAnswer = serverQuiz.Questions.FirstOrDefault(a => a.Id == q.Id);
                    var tempSolved = new SolvedQuestionDTO(q);

                    switch(q.Answer) 
                    {
                        case "A":
                        case "a":
                            if (serverAnswer.Answer.Equals(serverAnswer.OptionA, StringComparison.OrdinalIgnoreCase))
                            {
                                tempSolved.Marks = 5;
                            }
                            else 
                            {
                                tempSolved.Marks = 0;
                            }
                            break;
                            
                        case "B":
                        case "b":
                            if (serverAnswer.Answer.Equals(serverAnswer.OptionB, StringComparison.OrdinalIgnoreCase))
                            {
                                tempSolved.Marks = 5;
                            }
                            else
                            {
                                tempSolved.Marks = 0;
                            }
                            break; 
                        
                        case "C":
                        case "c":
                            if (serverAnswer.Answer.Equals(serverAnswer.OptionC, StringComparison.OrdinalIgnoreCase))
                            {
                                tempSolved.Marks = 5;
                            }
                            else
                            {
                                tempSolved.Marks = 0;
                            }
                            break;

                        case "D":
                        case "d":
                            if (serverAnswer.Answer.Equals(serverAnswer.OptionD, StringComparison.OrdinalIgnoreCase))
                            {
                                tempSolved.Marks = 5;
                            }
                            else
                            {
                                tempSolved.Marks = 0;
                            }
                            break;

                        default:
                            tempSolved.Marks = 0;
                            break;

                    }

                    returnValue.Answers.Add(tempSolved);
                }
            }
            

            return await Task.FromResult<QuizResultDTO>(returnValue);
        }
    }
}
