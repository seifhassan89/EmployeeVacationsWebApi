using employee_task.Models;

namespace employee_task.Mappers.Contracts
{
    public interface IUserMapper
    {
        public User MapToUser(UserDTO userDTO);
        public UserDTO MapToUserDTO(User user);

    }
}
