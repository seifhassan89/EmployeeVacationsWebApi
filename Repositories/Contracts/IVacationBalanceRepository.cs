using employee_task.Data;
using employee_task.Models;

namespace employee_task.Repositories.Contracts
{
    public interface IVacationBalanceRepository : IBaseRepository
    {
        public void DeleteVacationBalance(VacationBalance vacationBalance);
        public VacationBalance? GetVacationBalanceByUserIdAndVacationType(int userId, int vacationTypeId);
        public bool EditVacationBalance(int id, VacationBalance vacationBalance);
        public List<VacationBalance> GetAllByUserID(int userID);
        public VacationBalance? GetById(int id);

        public VacationBalance AddVacationBalance(VacationBalance vacationBalance);

    }
}
