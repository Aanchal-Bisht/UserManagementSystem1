using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Reflection.Metadata;
using Microsoft.Extensions.Options;

namespace UMSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class RegistrationController : ControllerBase { 

        private IOptions<LogFileConfig> _logConfig;
    public RegistrationController (IOptions<LogFileConfig> logConfig)
    {
        _logConfig = logConfig;
    }


    
        [HttpPost(Name = "PostUserDetails")]
        
       
        public int Registeration(string username, string email, string password, string dob, string Gender, string Department, string phone)
        {
         string RegLogfilePath = _logConfig.Value.LogFilePath;

            try { 

            SqlConnection con = new SqlConnection(@"Data Source=192.168.0.89;Initial Catalog=Userdb;User ID=sa;password=droisys@4800;TrustServerCertificate=true");
            SqlCommand cmd = new SqlCommand("InsertUser", con);
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
            LogWriter.LogWrite("User Sucsessfully Registered" + " " + username, RegLogfilePath);
                int k = cmd.ExecuteNonQuery();
                return 1;
                con.Close();

            }

            catch(Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }


           
           
        }
    }
}
