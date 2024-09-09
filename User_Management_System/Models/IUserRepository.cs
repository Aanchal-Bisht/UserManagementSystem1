using System.Data;
using System.Data.SqlClient;

namespace User_Management_System.Models
{
    public interface IUserRepository
    {
        void AddUser(User user);
        void UpdateUser(User user);
       // void DeleteUser(int userId);
       /* public DataTable getUserDetails(string uname, string pass);
      // public DataTable getUserList();
        public User DisplayUser(string username, string pass);*/
    }
}
