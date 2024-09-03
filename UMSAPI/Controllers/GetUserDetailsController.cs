using Microsoft.AspNetCore.Mvc;
using System.Data;

using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace UMSAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetUserDetailsController : Controller
    {
        [HttpGet(Name = "GetUserDetails")]
        public string Get(string userName, string Pass)
        {
           // User myUser1 = new User();
            SqlConnection con = new SqlConnection(@"Data Source=192.168.0.89;Initial Catalog=Userdb;User ID=sa;password=droisys@4800;TrustServerCertificate=true");
            SqlCommand cmd = new SqlCommand("userDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("userName", userName);
            cmd.Parameters.AddWithValue("pass", Pass);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            if(dataTable != null)
            {
                string json = JsonConvert.SerializeObject(dataTable);
                con.Close();
                return json;
            }

            
            else
            {

                return null;
            }
        }
       
    }
}
