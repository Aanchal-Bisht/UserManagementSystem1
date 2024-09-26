using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;
using User_Management_System.Models;
using System.Numerics;
using System.Reflection;
using Microsoft.Extensions.Options;
using static System.Net.WebRequestMethods;
using Newtonsoft.Json;
using UMSAPI;
using System;

namespace User_Management_System.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IOptions<CustomConfig> _customConfig;
        private readonly IOptions<LogFileConfig> _logFileConfig;
     
        private readonly string logFilePath = string.Empty;
        public User myUser;


        public UserController(ILogger<UserController> logger, IConfiguration configuration, IOptions<CustomConfig> customConfig, IOptions<LogFileConfig> logFileConfig)
        {
            _logger = logger;
            _configuration = configuration;
            _customConfig = customConfig;
            _logFileConfig = logFileConfig;
            logFilePath = _logFileConfig.Value.LogFilePath;
        }

        public IActionResult Index()
        {
            return View();
        }

        public string PatchUserApi(string userName, string email, string password, DateOnly dob, string gender, string department, string phone, string url)
        {
            try
            {
                var client = new HttpClient();

                var webRequest = new HttpRequestMessage(HttpMethod.Patch, url);

                var response = client.Send(webRequest);

                using var reader = new StreamReader(response.Content.ReadAsStream());

                return reader.ReadToEnd();
            }
            catch (Exception ex)
            {

                LogWriter.LogWrite("User_Management_System.Controllers.PatchUserApi.delete: Exception => " + ex.ToString(), logFilePath);


            }
            return null;
        }
        private string DeleteUserApi(int userId, string url)
        {

            try
            {

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Delete, url);
                var response = client.Send(request);
                using var reader = new StreamReader(response.Content.ReadAsStream());
                var list = reader.ReadToEnd();
                return list;
            }
            catch (Exception ex)
            {

                LogWriter.LogWrite("User_Management_System.Controllers.DeleteUserApi.delete: Exception => " + ex.ToString(), logFilePath);

            }
            return null;
        }

        [HttpPost]
        public IActionResult Index(string userId, string userName, string email, DateOnly dob, string gender, string department, string phone, string buttonType)
        {
            try
            {
                myUser = new User();
                myUser.UserId = Convert.ToInt32(userId);
                myUser.UserName = userName;
                myUser.Email = email;
                myUser.DOB = dob.ToString();
                myUser.Gender = gender;
                myUser.DeptName = department;
                myUser.Phone = phone;
                int id = Convert.ToInt32(myUser.UserId);

                if (buttonType == "delete")
                {
                    try
                    {
                        string url = _customConfig.Value.ApiBaseUrl+ "DeleteUser" + "?userId=" + userId;
                        var deleteApiResponseObject = DeleteUserApi(Convert.ToInt32(userId), url);
                        return View("Index");
                    }
                    catch (Exception e)
                    {
                        LogWriter.LogWrite("User_Management_System.Controllers.Index: Exception => " + e.ToString(), logFilePath);
                    }

                }
                if (buttonType == "save")
                {
                    try
                    {
                        string url = _customConfig.Value.ApiBaseUrl + "api/Updation" + "?userId=" + userId + "&username=" + userName + "&email=" + email + "&gender=" + gender + "&dob=" + dob + "&Department=" + department + "&phone=" + phone;
                        var patchApiResponseObject = PatchUserApi(userId, userName, email, dob, gender, department, phone, url);
                        return View("Index");
                    }
                    catch (Exception e)
                    {
                        LogWriter.LogWrite("User_Management_System.Controllers.Index: Exception => " + e.ToString(), logFilePath);

                    }
                }
                return View("Index");
            }
            catch (Exception e)
            {
                LogWriter.LogWrite("User_Management_System.Controllers.Index: Exception => " + e.ToString(), logFilePath);

            }
            return View("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult UserSignout()
        {

            return View();
        }
        // api hit for getting user details for admin user list 
        private string GetUserListFromApi(string url)
        {
            try
            {
                var client = new HttpClient();
                var webRequest = new HttpRequestMessage(HttpMethod.Get, url);
                var response = client.Send(webRequest);
                using var reader = new StreamReader(response.Content.ReadAsStream());
                var list = reader.ReadToEnd();
                return list;
            }
            catch (Exception e)
            {
                LogWriter.LogWrite("User_Management_System.Controllers.GetUserListFromApi.Get: Exception => " + e.ToString(), logFilePath);

            }
            return null;
        }
        public IActionResult UserList()

        {
            try
            {
                //calling api hitting method
                string url = _customConfig.Value.ApiBaseUrl+ "UserList";
                var userListFromApi = GetUserListFromApi(url);
                //converting json string to datatable object
                var dataTable = JsonConvert.DeserializeObject<DataTable>(userListFromApi);
                if (dataTable != null)
                {
                    ViewBag.DataTable = dataTable;
                }
                return View();
            }
            catch (Exception e)
            {
                LogWriter.LogWrite("User_Management_System.Controllers.UserList: Exception => " + e.ToString(), logFilePath);

            }
            return View();
        }
        // api hit for getting user details for login and dashboard 
        private string GetUserDetailsFromAPI(string userName, string password, string url)
        {
            //hit the api passing "userName, pwd"

            //receive api response and deserialize it from json to class object

            //return this class object
            try
            {
                var client = new HttpClient();

                var webRequest = new HttpRequestMessage(HttpMethod.Get, url);

                var response = client.Send(webRequest);

                using var reader = new StreamReader(response.Content.ReadAsStream());

                return reader.ReadToEnd();
            }
            catch (Exception e)
            {

                LogWriter.LogWrite("User_Management_System.Controllers.GetUserDetailsFromAPI.Get: Exception => " + e.ToString(), logFilePath);



            }
            return null;
        }

        [HttpPost]
        public IActionResult RedirectToHome(string userName, string password)
        {
            try
            {
                string url = _customConfig.Value.ApiBaseUrl+ "GetUserDetails" + "?userName=" + userName + "&Pass=" + password;
                //call the api method here
                var yourClassObject = GetUserDetailsFromAPI(userName, password, url);
                DataTable userListDataTable =( yourClassObject != null ? JsonConvert.DeserializeObject<DataTable>(yourClassObject) : null);
                myUser = new User();
                var rowCount = (userListDataTable != null ? userListDataTable.Rows.Count : 0);
                for (var i = 0; i < rowCount; i++)
                {

                    myUser.UserId = Convert.ToInt32(userListDataTable.Rows[i]["UserId"]);
                    myUser.UserName = userListDataTable.Rows[i]["UserName"].ToString();
                    myUser.Password = userListDataTable.Rows[i]["Pwd"].ToString();
                    myUser.Email = userListDataTable.Rows[i]["Email"].ToString();
                    myUser.Phone = userListDataTable.Rows[i]["ContactNo"].ToString();
                    myUser.Gender = userListDataTable.Rows[i]["Gender"].ToString();
                    myUser.DOB = userListDataTable.Rows[i]["DOB"].ToString();
                    myUser.DeptName = userListDataTable.Rows[i]["DeptId"].ToString();
                    myUser.RoleId = userListDataTable.Rows[i]["RoleId"].ToString();

                }
                if (rowCount > 0 && string.IsNullOrEmpty(myUser.RoleId))//common user
                {
                    TempData["user"] = userName;
                    TempData["userName"] = myUser.UserName;
                    TempData["userId"] = myUser.UserId;
                    TempData["Email"] = myUser.Email;
                    TempData["Gender"] = myUser.Gender;
                    DateTime dateObject = DateTime.Parse(myUser.DOB);
                    TempData["DOB"] = dateObject.Date;
                    TempData["Phone"] = myUser.Phone;
                    TempData["DeptName"] = myUser.DeptName;
                    TempData["UserRoleId"] = myUser.RoleId;
                    TempData["Check"] = true;
                    return RedirectToAction("Index");
                }
                else if (rowCount > 0 && !string.IsNullOrEmpty(myUser.RoleId))//admin user check
                {
                    TempData["UserRoleId"] = myUser.RoleId;//storing admin user's role-id

                    TempData["Check"] = true;//for identifying admin user on different pages

                    return RedirectToAction("UserList");//sending admin user to users-list as only admin user has access to this
                }

                else//invalid user
                {
                    ViewBag.Message = "Invalid user";
                    return View("Login");
                }
            }
            catch (Exception ex)
            {
                LogWriter.LogWrite("User_Management_System.Controllers.RedirectToHome: Exception => " + ex.ToString(), logFilePath);

            }
            return View("Login");
        }

        public IActionResult Register()
        {
            return View();
        }
        public string PostApiResponse(string userName, string email, string password, DateOnly dob, string gender, string department, string phone, string url)
        {
            try
            {
                var client = new HttpClient();

                var webRequest = new HttpRequestMessage(HttpMethod.Post, url);

                var response = client.Send(webRequest);

                using var reader = new StreamReader(response.Content.ReadAsStream());

                return reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                LogWriter.LogWrite("User_Management_System.Controllers.PostApiResponse: Exception => " + ex.ToString(), logFilePath);

            }
            return null;
        }

        [HttpPost]
        public IActionResult RedirectToLogin(string userName, string email, string password, DateOnly dob, string gender, string department, string phone)
        {
            try
            {
                string url = _customConfig.Value.ApiBaseUrl+ "api/Registration" + "?username=" + userName + "&email=" + email + "&password=" + password + "&gender=" + gender + "&dob=" + dob + "&Department=" + department + "&phone=" + phone;

                var classobject = PostApiResponse(userName, email, password, dob, gender, department, phone, url);

                myUser = new User();
                myUser.UserName = userName;
                myUser.Email = email;
                myUser.Password = password;
                myUser.DOB = dob.ToString();
                myUser.Gender = gender;
                myUser.DeptName = department;
                myUser.Phone = phone;
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                LogWriter.LogWrite("User_Management_System.Controllers.RedirectToLogin: Exception => " + ex.ToString(), logFilePath);
            }
            return RedirectToAction("Login");
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Services()

        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
