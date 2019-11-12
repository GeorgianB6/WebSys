using System;

namespace InterviewManagement
{
    public class Job
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Function  { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }

        public Job(int id, string name, string function, string description, DateTime publicationDate)
        {
            Id = id;
            Name = name;
            Function = function;
            Description = description;
            PublicationDate = publicationDate;
        }
    }
}