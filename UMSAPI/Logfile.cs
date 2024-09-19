namespace UMSAPI
{
    public class Logfile
    { 
            private static string m_exePath = string.Empty;
            

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

