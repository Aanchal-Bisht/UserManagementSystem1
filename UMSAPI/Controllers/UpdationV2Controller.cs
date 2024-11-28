using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Numerics;
using System.Reflection;

namespace UMSAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UpdationV2Controller : Controller
    {
        [HttpPatch] 
         public IActionResult updateUser([FromBody] UpdateUserBody user)
        {
            if (user == null)
            {
                return BadRequest("User Data is Required");
            }
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=192.168.0.89;Initial Catalog=Userdb;User ID=sa;password=droisys@4800;TrustServerCertificate=true");
                SqlCommand cmd = new SqlCommand("updateUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("userId", Convert.ToInt32(user.UserId));
                cmd.Parameters.AddWithValue("userName", user.UserName);
                cmd.Parameters.AddWithValue("email", user.Email);
                cmd.Parameters.AddWithValue("dob", user.DOB);
                cmd.Parameters.AddWithValue("gender", user.Gender);
                cmd.Parameters.AddWithValue("phone", user.ContactNo);
                cmd.Parameters.AddWithValue("dept", user.DeptId);
                con.Open();

                int k = cmd.ExecuteNonQuery();
                return Ok();
                con.Close();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ex.Message);

            }


        }


    }
}
