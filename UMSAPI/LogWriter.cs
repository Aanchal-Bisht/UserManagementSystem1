using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace UMSAPI
{
    public static class LogWriter
    {
        private static string m_exePath = string.Empty;
        public static void LogWrite(string logMessage ,string path)
        {

            // m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //m_exePath= "C:\\Users\\ParthBajaj\\source\\repos\\UserManagementSystem1\\UMSAPI\\LogFile";
            m_exePath = path;
            if (string.IsNullOrEmpty(m_exePath) == false)
            {

                if (!File.Exists(m_exePath + "\\" + "log.txt"))
                    File.Create(m_exePath + "\\" + "log.txt");

                try
                {
                    using (StreamWriter w = File.AppendText(m_exePath + "\\" + "log.txt"))
                        AppendLog(logMessage, w);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public static void DeleteLogWrite(string logMessage, string path)
        {
            m_exePath = path;
            if (string.IsNullOrEmpty(m_exePath) == false)
            {
                if (!File.Exists(m_exePath + "\\" + "log.txt"))
                    File.Create(m_exePath + "\\" + "log.txt");
                try
                {
                    using (StreamWriter w = File.AppendText(m_exePath + "\\" + "log.txt"))
                        AppendLog(logMessage, w);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        private static void AppendLog(string logMessage, TextWriter txtWriter)
        {
            try
            {
              //  txtWriter.Write("\r\nLog Entry : ");
                txtWriter.WriteLine("{0} {1} {2}",DateTime.Now.ToString("yyyy-MMM-dd"), DateTime.Now.ToLongTimeString(), logMessage);
              //  txtWriter.WriteLine("  :");
               // txtWriter.WriteLine("  :{0}", logMessage);
              //  txtWriter.WriteLine("-------------------------------");
            }
            catch (Exception ex)
            {
            }
        }
    }
}