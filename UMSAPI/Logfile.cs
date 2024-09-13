namespace UMSAPI
{
    public class Logfile
    { 
            private static string m_exePath = string.Empty;
            public static void LogWrite(string logMessage, string path)
            {

                // m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                //m_exePath= "C:\\Users\\ParthBajaj\\source\\repos\\UserManagementSystem1\\UMSAPI\\LogFile";
                m_exePath = path;
                if (string.IsNullOrEmpty(m_exePath) == false)
                {

                    if (!File.Exists(m_exePath + "\\" + "Reglog.txt"))
                        File.Create(m_exePath + "\\" + "Reglog.txt");

                    try
                    {
                        using (StreamWriter w = File.AppendText(m_exePath + "\\" + "Reglog.txt"))
                            AppendLog(logMessage, w);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

        public static void UpdateLogWrite(string logMessage, string path)
        {
            m_exePath = path;
            if (string.IsNullOrEmpty(m_exePath) == false)
            {

                if (!File.Exists(m_exePath + "\\" + "Updatelog.txt"))
                    File.Create(m_exePath + "\\" + "Updatelog.txt");

                try
                {
                    using (StreamWriter w = File.AppendText(m_exePath + "\\" + "Updatelog.txt"))
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
                    
                    txtWriter.Write("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString() );
                   
                    txtWriter.WriteLine("  :{0}", logMessage);
                    
                }
                catch (Exception ex)
                {
                }
            }
        }
    }

