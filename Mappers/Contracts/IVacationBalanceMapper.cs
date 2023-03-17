using employee_task.Models;

namespace employee_task.Mappers.Contracts
{
    public interface IVacationBalanceMapper
    {
        public VacationBalance MapToVacationBalance(VacationBalanceDTO vacationBalanceDTO);
        public VacationBalanceDTO MapToVacationBalanceDTO(VacationBalance vacationBalance);
    }
}
