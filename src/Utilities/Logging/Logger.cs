using System;

namespace CleanArchitectureSample.Utilities.Logging
{
    public class Logger
    {
        private static string _LogFolderPath = string.Empty;
        public static void Init(string logFolderPath = null)
        {
            if (!string.IsNullOrWhiteSpace(logFolderPath))
            {
                _LogFolderPath = logFolderPath;
            }
            else
            {
                _LogFolderPath = FileHandler.GetAppDefaultDirectory() + "/logs";
            }
        }
        public static void Trace(string message)
        {
            string logFilePath = string.Format("{0}/{1:yyyyMMdd}.trace.log", _LogFolderPath, DateTime.Today);
            string text =
                "--------------------------------------" + Environment.NewLine
                + "Timestamp: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + Environment.NewLine
                + message + Environment.NewLine;
            Write(logFilePath, message);
        }
        public static void Write(Exception ex)
        {
            
            string logFilePath = string.Format("{0}/{1:yyyyMMdd}.log", _LogFolderPath, DateTime.Today);
            string message = 
                "--------------------------------------" + Environment.NewLine
                + "Timestamp: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + Environment.NewLine
                + "Exception: " + ex.Message + Environment.NewLine
                + "Stack Trace: " + Environment.NewLine
                + ex.ToString() + Environment.NewLine
                + "--------------------------------------" + Environment.NewLine;
            Write(logFilePath, message);
        }
        public static void Write(string message)
        {
            string logFilePath = string.Format("{0}/{1:yyyyMMdd}.log", _LogFolderPath, DateTime.Today);
            Write(logFilePath, message);
        }
        public static void Write(string filePath, string message)
        {
            try
            {
                FileHandler.AppendAllText(filePath, message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
