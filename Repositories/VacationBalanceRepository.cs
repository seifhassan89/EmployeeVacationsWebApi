using employee_task.Data;
using employee_task.Models;
using employee_task.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace employee_task.Repositories
{
    public class VacationBalanceRepository : BaseRepository, IVacationBalanceRepository
    {
        private EmployeeDB _EmployeeDB;
        public VacationBalanceRepository(EmployeeDB employeeDB) : base(employeeDB)
        {
            _EmployeeDB = employeeDB;
        }

        /// <summary>
        /// Add vacation Balance to Database
        /// </summary>
        /// <param name="vacationBalance"></param>
        /// <returns></returns>
        public VacationBalance AddVacationBalance(VacationBalance vacationBalance)
        {
            EntityEntry<VacationBalance> x = _EmployeeDB.VacationBalances.Add(vacationBalance);
            return x.Entity;

        }

        /// <summary>
        /// delete vacation Balance
        /// </summary>
        /// <param name="vacationBalance"></param>
        public void DeleteVacationBalance(VacationBalance vacationBalance)
        {
            _EmployeeDB.Remove(vacationBalance);
        }

        /// <summary>
        ///  edit vacation Balance Data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="vacationBalanceBody"></param>
        /// <returns></returns>
        public bool EditVacationBalance(int id, VacationBalance vacationBalanceBody)
        {
            VacationBalance? vacationBalanceExisting = (from vacationbalance in _EmployeeDB.VacationBalances
                                                        where vacationbalance.VacationBalanceId == id
                                                        select vacationbalance).FirstOrDefault();

            if (vacationBalanceExisting == null)
            {
                return false;
            }
            else
            {
                _EmployeeDB.Entry(vacationBalanceExisting).State = EntityState.Detached;
            }
            _EmployeeDB.Attach(vacationBalanceBody);
            _EmployeeDB.Entry(vacationBalanceBody).State = EntityState.Modified;
            return true;

        }

        /// <summary>
        /// get vacation balance by user id and vacation type id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="vacationTypeId"></param>
        /// <returns></returns>
        public VacationBalance? GetVacationBalanceByUserIdAndVacationType(int userId, int vacationTypeId)
        {
            return _EmployeeDB.VacationBalances
                   .Where(b => b.UserId == userId && b.VacationTypeId == vacationTypeId).FirstOrDefault();

        }


        /// <summary>
        /// get vacation Balance by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VacationBalance? GetById(int id)
        {
            VacationBalance? vacationBalance = _EmployeeDB.VacationBalances
                .Include(x => x.VacationType)
                .FirstOrDefault(u => u.VacationBalanceId == id);
            return vacationBalance;
        }

        /// <summary>
        /// get all vacation Balance for a specific user
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<VacationBalance> GetAllByUserID(int userID)
        {
            List<VacationBalance> vacationBalances = _EmployeeDB.VacationBalances
              .Include(x => x.VacationType)
              .Where(u => u.UserId == userID)
              .ToList();
            return vacationBalances;
        }



    }
}
