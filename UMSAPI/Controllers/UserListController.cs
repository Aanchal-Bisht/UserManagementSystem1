using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
namespace UMSAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserListController : Controller
    {
        [HttpGet(Name = "GetUserList")]
        public string Get()
        {
            SqlConnection con = new SqlConnection(@"Data Source=192.168.0.89;Initial Catalog=Userdb;User ID=sa;password=droisys@4800;TrustServerCertificate=true");
            SqlCommand cmd = new SqlCommand("userList", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            string json = JsonConvert.SerializeObject(dataTable); 
            con.Close();
            return json;
        }
    }
}
