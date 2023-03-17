using employee_task.Data;
using employee_task.Models;
using employee_task.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace employee_task.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private EmployeeDB _EmployeeDB;
        public UserRepository(EmployeeDB employeeDB) : base(employeeDB)
        {
            _EmployeeDB = employeeDB;
        }
        /// <summary>
        /// Add User to Database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User AddUser(User user)
        {
            EntityEntry<User> x = _EmployeeDB.Users.Add(user);
            return x.Entity;

        }
        /// <summary>
        /// delete user
        /// </summary>
        /// <param name="user"></param>
        public void DeleteUser(User user)
        {
            _EmployeeDB.Remove(user);
        }

        /// <summary>
        /// edit user data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool EditUser(int userId, User user)
        {
            User? existingEntity = _EmployeeDB.Users.Find(userId);
            if (existingEntity == null)
            {
                return false;
            }
            else
            {
                _EmployeeDB.Entry(existingEntity).State = EntityState.Detached;
            }
            _EmployeeDB.Attach(user);
            _EmployeeDB.Entry(user).State = EntityState.Modified;
            return true;
        }
        /// <summary>
        /// get all users
        /// </summary>
        /// <returns></returns>
        public List<User> GetAll()
        {
            List<User> users = new List<User>();
            users = _EmployeeDB.Users
                    .Include(u => u.Gender)
                    .Include(u => u.Role)
                    .ToList();
            return users;
        }

        /// <summary>
        /// get user DTO by Id full data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserDTO? GetUserFullDetailsById(int id)
        {
            UserDTO? userDto = (from user in _EmployeeDB.Users

                                join gender in _EmployeeDB.Genders
                                on user.GenderId equals gender.GenderId

                                join role in _EmployeeDB.Roles
                                on user.RoleId equals role.RoleId

                                join vacationBalance in _EmployeeDB.VacationBalances
                                on user.UserId equals vacationBalance.UserId into userVacationBalances
                                from userVacationBalance in userVacationBalances.DefaultIfEmpty()

                                join vacationType in _EmployeeDB.VacationTypes
                                on userVacationBalance.VacationTypeId equals vacationType.VacationTypeId into balanceVacationTypes
                                from balanceVacationType in balanceVacationTypes.DefaultIfEmpty()


                                where user.UserId == id
                                select new { user, role, gender, userVacationBalance, balanceVacationType })
                                .GroupBy(tuple => new
                                {
                                    UserId = tuple.user.UserId,
                                    FullName = tuple.user.FullName,
                                    Email = tuple.user.Email,
                                    BirhthDate = tuple.user.BirhthDate,
                                    RoleId = tuple.user.RoleId,
                                    RoleName = tuple.role.Name,
                                    GenderId = tuple.user.GenderId,
                                    GenderName = tuple.gender.Name,

                                })
                                .Select(grp => new UserDTO
                                {

                                    UserId = grp.Key.UserId,
                                    FullName = grp.Key.FullName,
                                    Email = grp.Key.Email,
                                    BirhthDate = grp.Key.BirhthDate,
                                    RoleId = grp.Key.RoleId,
                                    Role = grp.Key.RoleName,
                                    GenderId = grp.Key.GenderId,
                                    Gender = grp.Key.GenderName,
                                    vacationBalanceDTOs = grp.Select(vacationBalanceObj => new VacationBalanceDTO
                                    {
                                        VacationBalanceId = vacationBalanceObj.userVacationBalance.VacationBalanceId,
                                        UserId = vacationBalanceObj.userVacationBalance.UserId,
                                        VacationTypeId = vacationBalanceObj.userVacationBalance.VacationTypeId,
                                        Balance = vacationBalanceObj.userVacationBalance.Balance,
                                        Used = vacationBalanceObj.userVacationBalance.Used,
                                        VacationType = vacationBalanceObj.balanceVacationType.Name,
                                    }).ToList()
                                }).FirstOrDefault();

            return userDto;
        }


        /// <summary>
        /// get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User? GetById(int id)
        {
            User? user = _EmployeeDB.Users
                .Include(u => u.VacationBalances)
                .Include(u => u.VacationRequests)
                .FirstOrDefault(u => u.UserId == id);
            return user;
        }

    }
}
