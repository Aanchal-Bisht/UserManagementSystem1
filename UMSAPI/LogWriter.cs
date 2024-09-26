using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace UMSAPI
{
    public static class LogWriter
    {
        private static string logFilePath = string.Empty;
        public static void LogWrite(string logMessage, string path)
        {

            // m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //m_exePath= "C:\\Users\\ParthBajaj\\source\\repos\\UserManagementSystem1\\UMSAPI\\LogFile";
            logFilePath = path;
            if (string.IsNullOrEmpty(logFilePath) == false)
            {

                if (!File.Exists(logFilePath + "\\" + "log.txt"))
                    File.Create(logFilePath + "\\" + "log.txt");

                try
                {
                    using (StreamWriter w = File.AppendText(logFilePath + "\\" + "log.txt"))
                        AppendLog(logMessage, w);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        //public static void DeleteLogWrite(string logMessage, string path)
        //{
        //    m_exePath = path;
        //    if (string.IsNullOrEmpty(m_exePath) == false)
        //    {
        //        if (!File.Exists(m_exePath + "\\" + "log.txt"))
        //            File.Create(m_exePath + "\\" + "log.txt");
        //        try
        //        {
        //            using (StreamWriter w = File.AppendText(m_exePath + "\\" + "log.txt"))
        //                AppendLog(logMessage, w);
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //        }
        //    }
        //}

        //public static void RegisterLogWrite(string logMessage, string path)
        //{

        //    // m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        //    //m_exePath= "C:\\Users\\ParthBajaj\\source\\repos\\UserManagementSystem1\\UMSAPI\\LogFile";
        //    m_exePath = path;
        //    if (string.IsNullOrEmpty(m_exePath) == false)
        //    {

        //        if (!File.Exists(m_exePath + "\\" + "log.txt"))
        //            File.Create(m_exePath + "\\" + "log.txt");

        //        try
        //        {
        //            using (StreamWriter w = File.AppendText(m_exePath + "\\" + "log.txt"))
        //                AppendLog(logMessage, w);
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //        }
        //    }
        //}

        //public static void UpdateLogWrite(string logMessage, string path)
        //{
        //    m_exePath = path;
        //    if (string.IsNullOrEmpty(m_exePath) == false)
        //    {

        //        if (!File.Exists(m_exePath + "\\" + "log.txt"))
        //            File.Create(m_exePath + "\\" + "log.txt");

        //        try
        //        {
        //            using (StreamWriter w = File.AppendText(m_exePath + "\\" + "log.txt"))
        //                AppendLog(logMessage, w);
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //        }
        //    }
        //}
        private static void AppendLog(string logMessage, TextWriter txtWriter)
        {
            try
            {
            
                txtWriter.WriteLine("{0} {1} {2}", DateTime.Now.ToString("yyyy-MMM-dd"), DateTime.Now.ToLongTimeString(), logMessage);
                
            }
            catch (Exception ex)
            {
            }
        }
    }
}