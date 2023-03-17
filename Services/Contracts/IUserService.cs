using employee_task.Models;

namespace employee_task.Services.Contracts
{
    public interface IUserService
    {
        public List<UserDTO> GetAll();
        public UserDTO? GetById(int id);
        public User AddUser(UserDTO user);
        public bool EditUser(int userId, UserDTO userDTO);
        public bool DeleteUser(int userId);
        public void SaveChanges();
    }
}
