using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace UMSAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeleteUserController : Controller
    {
        [HttpDelete (Name="DeleteUser")]
        public int Delete(int userId)
        {
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=192.168.0.89;Initial Catalog=Userdb;User ID=sa;password=droisys@4800;TrustServerCertificate=true");
                SqlCommand cmd = new SqlCommand("DelUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("userId", userId);
                con.Open();
                int k = cmd.ExecuteNonQuery();
                return 1;
                con.Close();
            }
            catch (Exception e)
            {
                return -1;  
            }
           
           
        }
        

    }
}
