using employee_task.Data;
using employee_task.Mappers;
using employee_task.Mappers.Contracts;
using employee_task.Models;
using employee_task.Repositories.Contracts;
using employee_task.Services.Contracts;
using System.Collections.Generic;

namespace employee_task.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IVacationBalanceRepository _vacationBalanceRepository;
        private readonly IVacationRequestRepository _vacationRequestRepository;
        private readonly IUserMapper _userMapper;
        private readonly IVacationTypeRepository _vacationTypeRepository;
        public UserService(IUserRepository userRepository, IUserMapper userMapper, IVacationTypeRepository vacationType)
        {
            _userRepository = userRepository;
            _userMapper = userMapper;
            _vacationTypeRepository = vacationType;
        }
        /// <summary>
        /// add user to DB
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User AddUser(UserDTO user)
        {
            User userEntity = _userMapper.MapToUser(user);
            List<VacationType> vacationTypes = _vacationTypeRepository.GetAll();

            foreach (VacationType vacationType in vacationTypes)
            {
                VacationBalance userVacationBalance = new VacationBalance();
                userVacationBalance.Balance = vacationType.IntialValue;
                userVacationBalance.VacationTypeId = vacationType.VacationTypeId;
                userVacationBalance.Used = 0;
                userEntity.VacationBalances.Add(userVacationBalance);
            }

            User AddUser = _userRepository.AddUser(userEntity);

            return AddUser;
        }

        /// <summary>
        /// delete user by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool DeleteUser(int userId)
        {
            User? user = _userRepository.GetById(userId); ;
            if (user == null)
            {
                return false;
            }
            user.VacationRequests.ForEach(vacationRequest =>
            {
                _vacationRequestRepository.DeleteVacationRequest(vacationRequest);
            });
            user.VacationBalances.ForEach(vacationBalanceRequest =>
            {
                _vacationBalanceRepository.DeleteVacationBalance(vacationBalanceRequest);
            });

            _userRepository.DeleteUser(user);
            return true;
        }

        /// <summary>
        /// edit user data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userDTO"></param>
        public bool EditUser(int userId, UserDTO userDTO)
        {
            User userEntity = _userMapper.MapToUser(userDTO);
            return _userRepository.EditUser(userId, userEntity);
        }

        /// <summary>
        /// get all users DTO
        /// </summary>
        /// <returns></returns>
        public List<UserDTO> GetAll()
        {
            List<UserDTO> users = new List<UserDTO>();

            List<User> userEntities = _userRepository.GetAll();
            userEntities.ForEach(userEntity =>
            {
                //call mapper 
                UserDTO userDTO = _userMapper.MapToUserDTO(userEntity);
                users.Add(userDTO);
            });

            return users;
        }

        /// <summary>
        /// get user DTO by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserDTO? GetById(int id)
        {
            UserDTO? user = _userRepository.GetUserFullDetailsById(id);
            return user;

        }

        public void SaveChanges()
        {
            _userRepository.SaveChanges();
        }
    }
}
