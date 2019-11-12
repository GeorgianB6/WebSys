using System;

namespace InterviewManagement
{
    public class Application
    {
        public int Id { get; set; }
        public Applicant Applicant { get; set; }
        public Job Job { get; set; }
        public DateTime ApplicatioDate { get; set; }
        public bool CvEvaluation { get; set; }
        public bool TestResult { get; set; }
        public bool ApplicationFinalResult { get; set; }

        public Application(int id, Applicant applicant, Job job, DateTime applicatioDate, bool cvEvaluation, bool testResult, bool applicationFinalResult)
        {
            Id = id;
            Applicant = applicant;
            Job = job;
            ApplicatioDate = applicatioDate;
            CvEvaluation = cvEvaluation;
            TestResult = testResult;
            ApplicationFinalResult = applicationFinalResult;
        }
    }
}