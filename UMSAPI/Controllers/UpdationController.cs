using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace UMSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdationController : ControllerBase
    {
        private IOptions<LogFileConfig> _logConfig;
        public UpdationController(IOptions<LogFileConfig> logConfig)
        {
            _logConfig = logConfig;
        }


        [HttpPatch(Name = "PatchUserDetails")]
        public int Patch(string userId, string userName, string email, string dob, string gender, string department, string phone)
        {
            string regLogFilePath = _logConfig.Value.LogFilePath;
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=192.168.0.89;Initial Catalog=Userdb;User ID=sa;password=droisys@4800;TrustServerCertificate=true");
                SqlCommand cmd = new SqlCommand("updateUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("userId", userId);
                cmd.Parameters.AddWithValue("userName", userName);
                cmd.Parameters.AddWithValue("email", email);
                cmd.Parameters.AddWithValue("dob", DateOnly.Parse(dob));
                cmd.Parameters.AddWithValue("gender", gender);
                cmd.Parameters.AddWithValue("phone", phone);
                cmd.Parameters.AddWithValue("dept", department);
                con.Open();
                LogWriter.LogWrite("User Updated Successfully" + " " + userName, regLogFilePath);

                int k = cmd.ExecuteNonQuery();
                return 1;
                con.Close();
            }
            catch (Exception ex)
            {
                {
                    LogWriter.LogWrite("UMSAPI.Controllers.UpdationController.Patch: Exception => " + ex.ToString(), regLogFilePath);
                    return -1;
                }
            }
        }
    }
}
