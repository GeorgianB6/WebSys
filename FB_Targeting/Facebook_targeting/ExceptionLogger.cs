using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Facebook_targeting
{
    public static class ExceptionLogger
    {
        //private static readonly bool LogException;
        private static readonly string XmlLogFilePath;
       
        static ExceptionLogger()
        {
            XmlLogFilePath = FacebookService.Configuration["ExceptionLogPath"];
        }

        public static void Log(Exception exception, string source)
        {
            //if (!LogException) return;
            LogExceptionFileStream(exception, source);
        }

        private static void LogExceptionFileStream(Exception exception, string source)
        {
            if (string.IsNullOrEmpty(XmlLogFilePath))
                return;

            if (!Directory.Exists(XmlLogFilePath))
                return;

            try
            {
                var exceptionContent = BuildErrorLog(exception, source);

                WriteToFile(exceptionContent);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private static void WriteToFile(string exceptionContent)
        {
            var file = $"{XmlLogFilePath}{DateTime.Now.Day}{DateTime.Now.Month}{DateTime.Now.Year}log.txt";

            using (var f = new FileStream(file, FileMode.Append, FileAccess.Write))
            using (var ssw = new StreamWriter(f))
            {
                ssw.WriteLine(exceptionContent);
            }
        }

        private static string BuildErrorLog(Exception ex, string source)
        {

            return $"[{DateTime.UtcNow:s}] Source: {source} Message: {ex.Message} Trace: {ex.StackTrace} \n\n";
        }
    }
}


