using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

// class명 얻는법 - nameof(클래스 이름)
// 함수명 얻는 법 - System.Reflection.MethodBase.GetCurrentMethod().Name
namespace SmartFactoryProject_Final.Common
{
    class Log
    {
        private static ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();
        public enum LogType { Start, Process, End, Error }

        /// <summary>
        /// 로그를 파일, DB에 작성하는 함수
        /// </summary>
        /// <param name="type">로그의 타입</param>
        /// <param name="className">로그를 사용하는 Class</param>
        /// <param name="funcName">로그를 사용하는 함수</param>
        /// <param name="text">로그의 내용</param>
        public static void WriteLog(LogType type, string className, string funcName, string text)
        {
            WriteLog_File(type, className, funcName, text);
            WriteLog_DB(type, className, funcName, text);
        }

        /// <summary>
        /// 로그 파일에 로그를 작성하는 함수
        /// </summary>
        /// <param name="type">로그의 타입</param>
        /// <param name="className">로그를 사용하는 Class</param>
        /// <param name="funcName">로그를 사용하는 함수</param>
        /// <param name="text">로그의 내용</param>
        public static void WriteLog_File(LogType type, string className, string funcName, string text)
        {
            try
            {
                rwLock.EnterWriteLock();

                //폴더 접근
                string logFolderPath = Path.Combine(Application.StartupPath, "Logs", DateTime.Now.ToString("yyyy-MM"));
                if (!Directory.Exists(logFolderPath))
                    Directory.CreateDirectory(logFolderPath);

                //로그 파일 접근
                string logFileName = DateTime.Now.ToString("yyyy-MM-dd") + ".log";
                string logFilePath = Path.Combine(logFolderPath, logFileName);

                using(TextWriter stream = File.Exists(logFilePath)? File.AppendText(logFilePath) : File.CreateText(logFilePath))
                {
                    string logTime = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss.fff");
                    string logText = logTime + $" : {GetLogText(type, className, funcName, text)}" + Environment.NewLine;
                    stream.WriteLine(logText);
                    stream.Flush();
                    stream.Close();
                }
            }
            catch
            {

            }
            finally
            {
                if(rwLock.IsWriteLockHeld)
                    rwLock.ExitWriteLock();
            }
        }

        public static void WriteLog_DB(LogType type, string className, string funcName, string text)
        {

        }

        /// <summary>
        /// 로그 파일에 기록할 문자열을 리턴하는 함수
        /// </summary>
        /// <param name="type">로그의 타입</param>
        /// <param name="className">로그를 사용하는 Class</param>
        /// <param name="funcName">로그를 사용하는 함수</param>
        /// <param name="text">로그의 내용</param>
        /// <returns></returns>
        public static string GetLogText(LogType type, string className, string funcName, string text)
        {
            string logText = string.Empty;
            switch (type)
            {
                case LogType.Start:
                    logText = "<----------------------------------------------------- [PROGRAM][START] ----------------------------------------------------->";
                    break;
                case LogType.Process:
                    logText = $"[{className}][{funcName}]";
                    logText = logText.PadRight(58, ' ');
                    logText = $"{logText} => {text}";
                    break;
                case LogType.End:
                    logText = "<----------------------------------------------------- [PROGRAM][ END ] ----------------------------------------------------->";
                    break;
                case LogType.Error:
                    logText = $"★★★★★[Exception][{className}][{funcName}]";
                    logText = logText.PadRight(58, ' ');
                    logText = $"{logText} => {text}";
                    break;
            }

            return logText;
        }
    }
}
