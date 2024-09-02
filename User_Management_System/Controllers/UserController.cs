using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;
using User_Management_System.Models;
using System.Numerics;
using System.Reflection;

namespace User_Management_System.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IConfiguration _configuration;

        public User myUser;
        UserRepository newUser = new UserRepository();



        public UserController(ILogger<UserController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;

        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string userId, string username, string email, DateOnly dob, string gender, string department, string phone, string buttontype)
        {
            myUser = new User();
            myUser.UserId = Convert.ToInt32(userId);
            myUser.UserName = username;
            myUser.Email = email;
            myUser.DOB = dob.ToString();
            myUser.Gender = gender;
            myUser.DeptName = department;
            myUser.Phone = phone;
            Console.WriteLine(myUser.DeptName);
            //TempData["newuser"] = myUser;
            int id = Convert.ToInt32(myUser.UserId);
            if (buttontype == "save")
            {
                newUser.UpdateUser(myUser);
            }
            if (buttontype == "delete")
            {
                newUser.DeleteUser(id);
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
        public IActionResult UserList()

        {
            DataTable dt = newUser.getUserList();
            if (dt != null)
            {
                ViewBag.DataTable = dt;
            }
            return View();
        }

        private string GetUserDetailsFromAPI(string userName, string pwd)
        {
            //hit the api passing "userName, pwd"

            //receive api response and deserialize it from json to class object

            //return this class object
            return "";
           
        }

        [HttpPost]
        public IActionResult RedirectToHome(string userName, string pwd)
        {
            //call the api method here
            var yourClassObject = GetUserDetailsFromAPI(userName, pwd);

            //prepare your model calss object using yourClassObject

            User myu1 = new User();
            DataTable res = newUser.getUserDetails(userName, pwd);
            myu1 = newUser.DisplayUser(userName, pwd);
            
            // Console.WriteLine(myu1.RoleId);
            if (res.Rows.Count > 0 && string.IsNullOrEmpty(myu1.RoleId))
            {
                TempData["user"] = userName;
                // User myu1 =newUser.DisplayUser(userName, pwd);
                Console.WriteLine(myu1.UserName);
                TempData["userName"] = myu1.UserName;
                TempData["userId"] = myu1.UserId;
                TempData["Email"] = myu1.Email;
                TempData["Gender"] = myu1.Gender;
                DateTime dateObject = DateTime.Parse(myu1.DOB);
                TempData["DOB"] = dateObject.Date;
                // Console.WriteLine(TempData["DOB"]); 
                TempData["Phone"] = myu1.Phone;
                TempData["DeptName"] = myu1.DeptName;
                TempData["UserRoleId"] = myu1.RoleId;
               
                TempData["Check"] = true;
                // Console.WriteLine(TempData["userId"]);
                return RedirectToAction("Index");
            }
            else if (res.Rows.Count > 0 && !string.IsNullOrEmpty(myu1.RoleId))
            {
                TempData["UserRoleId"] = myu1.RoleId;
                
                TempData["Check"] = true;
                return RedirectToAction("UserList");
            }

            else
            {

                ViewBag.Message = "Invalid user";
                return View("Login");
            }

        }

        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult RedirectToLogin(string username, string email, string password, DateOnly dob, string Gender, string Department, string phone)
        {
            myUser = new User();
            myUser.UserName = username;
            myUser.Email = email;
            myUser.Password = password;
            myUser.DOB = dob.ToString();
            myUser.Gender = Gender;
            myUser.DeptName = Department;
            myUser.Phone = phone;

            //TempData["newuser"] = myUser;
            newUser.AddUser(myUser);
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
