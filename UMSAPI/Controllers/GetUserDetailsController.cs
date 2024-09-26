using Microsoft.AspNetCore.Mvc;
using System.Data;

using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;

namespace UMSAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetUserDetailsController : Controller
    {
        private IOptions<LogFileConfig> _logConfig;
        public GetUserDetailsController(IOptions<LogFileConfig> logConfig)
        {
            _logConfig = logConfig;

        }
      
        [HttpGet(Name = "GetUserDetails")]
        public string Get(string userName, string pass)
        {
            string logFilePath = _logConfig.Value.LogFilePath;
            try
            {
               
                SqlConnection con = new SqlConnection(@"Data Source=192.168.0.89;Initial Catalog=Userdb;User ID=sa;password=droisys@4800;TrustServerCertificate=true");
                SqlCommand cmd = new SqlCommand("userDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("userName", userName);
                cmd.Parameters.AddWithValue("pass", pass);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable); 
                if (dataTable.Rows.Count>0)
                {
                    LogWriter.LogWrite("User found: userName=" + userName,logFilePath);
                    string json = JsonConvert.SerializeObject(dataTable);
                    con.Close();
                    return json;
                }
                else
                {
                    LogWriter.LogWrite("User not found: userName=" + userName, logFilePath);
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogWriter.LogWrite("UMSAPI.Controllers.GetUserDetailsController.Get: Exception => " + ex.ToString(),logFilePath);
                return null;
            }
        }

    }
}
