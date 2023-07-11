using System.Text.Json.Serialization;
using System.Text.Json;
using System.Runtime.InteropServices;
using McqsUI.Models;

namespace McqsUI.Utils
{
    // Native/JsonFileUtils.cs
    public static class JsonFileUtils
    {
        private static readonly JsonSerializerOptions _options =
            new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

        public static void SimpleWrite(object obj, string fileName)
        {
            var jsonString = JsonSerializer.Serialize(obj, _options);
            File.WriteAllText(fileName, jsonString);
        }

        public static void PrettyWrite(object obj, string fileName)
        {
            var options = new JsonSerializerOptions(_options)
            {
                WriteIndented = true
            };
            var jsonString = JsonSerializer.Serialize(obj, options);
            File.WriteAllText(fileName, jsonString);
        }

        public static QuizDTO[] ReadAllData(string fileName) 
        {
            var jsonString = File.ReadAllText(fileName);

            var quizList = JsonSerializer.Deserialize<QuizDTO[]>(jsonString);

            return quizList == null ? new List<QuizDTO>().ToArray() : quizList;
        }

        public static QuizDTO GetQuizbyId(string fileName, long id)
        {
            var jsonString = File.ReadAllText(fileName);

            var quizList = JsonSerializer.Deserialize<QuizDTO[]>(jsonString);
            if (quizList!=null)
            {
                var result = quizList.FirstOrDefault(q => q.Id == id);

                return result == null ? new QuizDTO() : result;
            }
            else 
            {
                return new QuizDTO();
            }
        }
    }
}
