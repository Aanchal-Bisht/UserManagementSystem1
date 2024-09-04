/*using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
//using System.Configuration;

namespace User_Management_System.Models
{
    public class UserRepository : IUserRepository
    {
        private SqlConnection con;
        //private readonly IConfiguration _configuration;

        private void connection()
        {
            *//*string c = Directory.GetCurrentDirectory();
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(c).AddJsonFile("appsettings.json").Build();
            string? connectionString = configuration.GetConnectionString("getconn");

            con = new SqlConnection(connetionString);


*//*
            con = new SqlConnection(@"Data Source=192.168.0.89;Initial Catalog=Userdb;User ID=sa;password=droisys@4800");

        }

       *//* public void AddUser(User user)
        {
            connection();
            SqlCommand cmd = new SqlCommand("InsertUser", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("userName", user.UserName);
            cmd.Parameters.AddWithValue("email", user.Email);
            cmd.Parameters.AddWithValue("pass", user.Password);
            cmd.Parameters.AddWithValue("DOB", user.DOB);
            cmd.Parameters.AddWithValue("gen", user.Gender);
            cmd.Parameters.AddWithValue("contact", user.Phone);
            cmd.Parameters.AddWithValue("DeptId", user.DeptName);
            cmd.Parameters.AddWithValue("RoleId", user.RoleId);
            con.Open();
            int k = cmd.ExecuteNonQuery();
            if (k != 0)
            {
                Console.WriteLine("Record Inserted Succesfully into the Database");
            }
            con.Close();
            return;
        }*/

        /*public void UpdateUser(User myUser)
        {
            connection();
            SqlCommand cmd = new SqlCommand("updateUser", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("userId", myUser.UserId);
            cmd.Parameters.AddWithValue("userName", myUser.UserName);
            cmd.Parameters.AddWithValue("email", myUser.Email);
            cmd.Parameters.AddWithValue("dob", myUser.DOB);
            cmd.Parameters.AddWithValue("gender", myUser.Gender);
            cmd.Parameters.AddWithValue("phone", myUser.Phone);
            cmd.Parameters.AddWithValue("dept", myUser.DeptName);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return;


        }
       /* public void DeleteUser(int userId)
        {
        }*//*
        public void DeleteUser(int userId) {
            connection();
            SqlCommand cmd = new SqlCommand("DelUser", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("userId", userId);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            return;
        }
        *//* public DataTable getUserDetails(string uname, string pass)
         {
             connection();
             SqlCommand cmd = new SqlCommand("userDetails",con);
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.Parameters.AddWithValue("userName", uname);
             cmd.Parameters.AddWithValue("pass", pass);

             con.Open();
             SqlDataAdapter adapter = new SqlDataAdapter(cmd);
             DataTable dataTable = new DataTable();
             adapter.Fill(dataTable);
             con.Close();
             return dataTable;
         }*//*
        //public DataTable getUserList()
        //{
        //    connection();
        //    SqlCommand cmd = new SqlCommand("userList", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    con.Open();
        //    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //    DataTable dataTable = new DataTable();
        //    adapter.Fill(dataTable);
        //    con.Close();
        //    return dataTable;
        //}
        *//* public User DisplayUser(string username, string pass)
         {
             User myUser1 = new User();
             SqlConnection con = new SqlConnection(@"Data Source=192.168.0.89;Initial Catalog=Userdb;User ID=sa;password=droisys@4800");
             SqlCommand cmd = new SqlCommand("userDetails", con);
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.Parameters.AddWithValue("userName", username);
             cmd.Parameters.AddWithValue("pass", pass);
             con.Open();
             SqlDataReader dataReader = cmd.ExecuteReader();

             if (dataReader.Read())
             {
                 //  Console.WriteLine(dataReader["UserId"].ToString());
                 myUser1.UserId = Convert.ToInt32(dataReader["UserId"]);

                 // Console.WriteLine(myUser1.UserId);
                 //Console.WriteLine(dataReader["UserId"].ToString());
                 //Console.WriteLine(myUser1.UserId);
                 myUser1.UserName = dataReader["UserName"].ToString();
                 myUser1.Email = dataReader["Email"].ToString();
                 myUser1.DOB = dataReader["DOB"].ToString();
                 myUser1.Gender = dataReader["Gender"].ToString();
                 myUser1.Phone = dataReader["ContactNo"].ToString();
                 myUser1.DeptName = dataReader["DeptId"].ToString();
                 myUser1.RoleId = dataReader["RoleId"].ToString();
                 Console.WriteLine(dataReader["RoleId"].ToString());
                 //TempData["username"] = dataReader["ussername"]
                 con.Close();
                 return myUser1;

             }
             else
             {
                 con.Close();
                 return myUser1;
             }
         }*//*
    }
}
*/