using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace UMSAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrationV2Controller : Controller
    {
            [HttpPost]
            public IActionResult Register([FromBody] RegisterUser user)
            {
                if (user == null)
                {
                    return BadRequest("User Data is Required");
                }
                try
                {
                    SqlConnection con = new SqlConnection(@"Data Source=192.168.0.89;Initial Catalog=Userdb;User ID=sa;password=droisys@4800;TrustServerCertificate=true");
                    SqlCommand cmd = new SqlCommand("InsertUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("userName", user.UserName);
                    cmd.Parameters.AddWithValue("email", user.Email);
                    cmd.Parameters.AddWithValue("pass", user.Password);
                    cmd.Parameters.AddWithValue("DOB", DateOnly.Parse(user.DOB));
                    cmd.Parameters.AddWithValue("gen", user.Gender);
                    cmd.Parameters.AddWithValue("contact", user.Phone);
                    cmd.Parameters.AddWithValue("DeptId", user.DeptId);
                    con.Open();

                    int k = cmd.ExecuteNonQuery();
                    return Ok();
                    con.Close();

                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error creating new employee record");

                }

            }
        }
    }

