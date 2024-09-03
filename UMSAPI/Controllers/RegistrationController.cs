using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.Data.SqlClient;

namespace UMSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        [HttpPost(Name ="PostUserDetails")]

        public int Post(string username, string email, string password, string dob, string Gender, string Department, string phone)
        {
           

          SqlConnection con = new SqlConnection(@"Data Source=192.168.0.89;Initial Catalog=Userdb;User ID=sa;password=droisys@4800;TrustServerCertificate=true");
            SqlCommand cmd= new SqlCommand("InsertUser", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("userName", username);
            cmd.Parameters.AddWithValue("email", email);
            cmd.Parameters.AddWithValue("pass", password);
            cmd.Parameters.AddWithValue("DOB", DateOnly.Parse(dob));
            cmd.Parameters.AddWithValue("gen", Gender);
            cmd.Parameters.AddWithValue("contact", phone);
            cmd.Parameters.AddWithValue("DeptId", Department);
         //   cmd.Parameters.AddWithValue("RoleId", RoleId);
            con.Open();
            int k = cmd.ExecuteNonQuery();
            if (k != 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
            con.Close();
           
        }
    }
}
