using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace UMSAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SingleUserDetailsController : Controller
    {
        [HttpGet]
        public string loginUserDetails(int id)
        {
            SqlConnection con = new SqlConnection(@"Data Source=192.168.0.89;Initial Catalog=Userdb;User ID=sa;password=droisys@4800;TrustServerCertificate=true");
            SqlCommand cmd = new SqlCommand("DetailsUser", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("userId", id);
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
