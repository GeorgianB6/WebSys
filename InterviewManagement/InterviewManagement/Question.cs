using System.Collections.Generic;

namespace InterviewManagement
{
    public class Question
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public Dictionary<string, bool> Answers { get; set; }

        public Question(int id, string content, Dictionary<string, bool> answers)
        {
            Id = id;
            Content = content;
            Answers = answers;
        }
    }
}