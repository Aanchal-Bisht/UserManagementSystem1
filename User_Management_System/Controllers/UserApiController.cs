using Microsoft.AspNetCore.Mvc;

namespace User_Management_System.Controllers
{
    public class UserApiController : Controller
    {
        public IActionResult AddUser()
        {
            //calling the user api by sending json request then receiving json response

            return View();
        }
    }
}
