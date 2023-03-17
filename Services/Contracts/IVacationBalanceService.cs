using employee_task.Models;

namespace employee_task.Services.Contracts
{
    public interface IVacationBalanceService
    {
        List<VacationBalanceDTO> GetAllByUserID(int userID);
        public VacationBalanceDTO? GetById(int id);
        public VacationBalance AddVacationBalance(VacationBalanceDTO vacationBalance);
        public bool EditVacationBalaceByVacationBalanceId(int vacationBalanceId, VacationBalanceDTO vacationBalanceBody);
        public bool UpdateBalance(VacationRequest vacationRequest);
        public bool DeleteVacationBalance(int vacationBalanceId);
        public void SaveChanges();
    }
}
