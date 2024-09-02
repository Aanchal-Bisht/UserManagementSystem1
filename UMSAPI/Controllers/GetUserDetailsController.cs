using Microsoft.AspNetCore.Mvc;
using System.Data;

using Microsoft.Data.SqlClient;

namespace UMSAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetUserDetailsController : Controller
    {
        [HttpGet(Name = "GetUserDetails")]
        public User Get(string userName, string Pass)
        {
            User myUser1 = new User();
            SqlConnection con = new SqlConnection(@"Data Source=192.168.0.89;Initial Catalog=Userdb;User ID=sa;password=droisys@4800;TrustServerCertificate=true");
            SqlCommand cmd = new SqlCommand("userDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("userName", userName);
            cmd.Parameters.AddWithValue("pass", Pass);
            con.Open();
            SqlDataReader dataReader = cmd.ExecuteReader();

            if (dataReader.Read())
            {
                myUser1.UserId = Convert.ToInt32(dataReader["UserId"]);
                myUser1.UserName = dataReader["UserName"].ToString();
                myUser1.Email = dataReader["Email"].ToString();
                myUser1.DOB = dataReader["DOB"].ToString();
                myUser1.Gender = dataReader["Gender"].ToString();
                myUser1.Phone = dataReader["ContactNo"].ToString();
                myUser1.DeptName = dataReader["DeptId"].ToString();
                myUser1.RoleId = dataReader["RoleId"].ToString();
                Console.WriteLine(dataReader["RoleId"].ToString());

                return myUser1;

            }
            else
            {

                return myUser1;
            }
        }
       
    }
}
