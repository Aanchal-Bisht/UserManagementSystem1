using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace UMSAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeleteUserController : Controller
    {   
        private readonly IOptions<LogFileConfig> _logFileConfig;
        public DeleteUserController(IOptions<LogFileConfig> logFileConfig)
        {
            _logFileConfig = logFileConfig;
        }
       
        [HttpDelete (Name="DeleteUser")]
        public int Delete(int userId)
        {
            string path=_logFileConfig.Value.LogFilePath;
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=192.168.0.89;Initial Catalog=Userdb;User ID=sa;password=droisys@4800;TrustServerCertificate=true");
                SqlCommand cmd = new SqlCommand("DelUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("userId", userId);
                con.Open();
                int k = cmd.ExecuteNonQuery();
                LogWriter.LogWrite("UserDeleted Successfully--User_Id: "+userId, path);
                return 1;
                con.Close();
            }
            catch (Exception e)
            {    
                LogWriter.LogWrite("UMSAPI.Controllers.DeleteUserController.Get: Exception => " + e.ToString(),path);
                return -1;  
            }
           
           
        }
        

    }
}
