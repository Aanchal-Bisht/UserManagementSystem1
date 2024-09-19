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
        public int Patch(string UserId, string username, string email, string dob, string Gender, string Department, string phone)
        {
            string ReglogFilePath = _logConfig.Value.LogFilePath;
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=192.168.0.89;Initial Catalog=Userdb;User ID=sa;password=droisys@4800;TrustServerCertificate=true");
                SqlCommand cmd = new SqlCommand("updateUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("userId", UserId);
                cmd.Parameters.AddWithValue("userName", username);
                cmd.Parameters.AddWithValue("email", email);
                cmd.Parameters.AddWithValue("dob", DateOnly.Parse(dob));
                cmd.Parameters.AddWithValue("gender", Gender);
                cmd.Parameters.AddWithValue("phone", phone);
                cmd.Parameters.AddWithValue("dept", Department);
                con.Open();
                LogWriter.UpdateLogWrite("User Updated Successfully" + " " + username, ReglogFilePath);

                int k = cmd.ExecuteNonQuery();
                return 1;
                con.Close();
            }
            catch (Exception e)
            {
                {
                    return -1;
                }




            }
        }
    }
}
