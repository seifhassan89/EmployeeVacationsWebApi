using employee_task.Mappers.Contracts;
using employee_task.Models;

namespace employee_task.Mappers
{
    public class UserMapper : IUserMapper
    {
        public UserDTO MapToUserDTO(User user)
        {
            UserDTO userDTO = new UserDTO();

            userDTO.UserId = user.UserId;
            userDTO.GenderId = user.GenderId;
            userDTO.Gender = user.Gender != null ? user.Gender?.Name : "";
            userDTO.Role = user.Role != null ? user.Role?.Name : "";
            userDTO.BirhthDate = user.BirhthDate;
            userDTO.FullName = user.FullName;
            userDTO.Email = user.Email;
            userDTO.RoleId = user.RoleId;
            return userDTO;
        }

        public User MapToUser(UserDTO userDTO)
        {
            User user = new User();

            user.UserId = userDTO.UserId;
            user.GenderId = userDTO.GenderId;
            user.BirhthDate = userDTO.BirhthDate;
            user.FullName = userDTO.FullName;
            user.Email = userDTO.Email;
            user.RoleId = userDTO.RoleId;

            return user;
        }
    }
}
