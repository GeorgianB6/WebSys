using System;
using System.Collections.Generic;


namespace InterviewManagement
{
    class Test
    {
        public int Id { get; set; }
        public Application Application { get; set; }
        public DateTime TestDate { get; set; }
        public decimal TestMark { get; set; }
        public List<Question> TestQuestions { get; set; }
 
        public Test(int id, Application application, DateTime testDate, decimal testMark)
        {
            Id = id;
            Application = application;
            TestDate = testDate;
            TestMark = testMark;
        }
    }
}
