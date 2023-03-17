using employee_task.Data;
using employee_task.Models;

namespace employee_task.Repositories.Contracts
{
    public interface IUserRepository : IBaseRepository
    {
        public List<User> GetAll();
        public UserDTO? GetUserFullDetailsById(int id);
        public User? GetById(int id);

        public User AddUser(User user);
        public bool EditUser(int userId, User user);
        public void DeleteUser(User user);
    }
}
