using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace UMSAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AuthenticateController : ControllerBase
    {

        [HttpPost]
        public IActionResult Authenticate([FromBody] LoginUser user)
        {
            try
            {

                if (user == null)
                {
                    return BadRequest("enter the data please");
                }
                else
                {
                    SqlConnection con = new SqlConnection(@"Data Source=192.168.1.43;Initial Catalog=Userdb;User ID=sa;password=droisys@4800;TrustServerCertificate=true");
                    SqlCommand cmd = new SqlCommand("login", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("userName", user.userName);
                    cmd.Parameters.AddWithValue("password", user.password);
                    con.Open();
                    //execute the command and retrieve the result 
                    var result = cmd.ExecuteScalar();//execute the stored procedure and returns the result 
                    if (result != null && (int)result == 1)
                    {
                        return Ok(new { msg = "Login successfull" });
                    }
                    else
                    {
                        return Unauthorized(new { message = "Invalid credentials" });
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal Server Error", details = ex.Message });
            }

        }
    }
}
