using employee_task.Mappers.Contracts;
using employee_task.Models;

namespace employee_task.Mappers
{
    public class VacationBalanceMapper : IVacationBalanceMapper
    {

        public VacationBalanceDTO MapToVacationBalanceDTO(VacationBalance vacationBalance)
        {

            VacationBalanceDTO vacationBalanceDTO = new VacationBalanceDTO();
            vacationBalanceDTO.VacationBalanceId = vacationBalance.VacationBalanceId;
            vacationBalanceDTO.Balance = vacationBalance.Balance;
            vacationBalanceDTO.Used = vacationBalance.Used;
            vacationBalanceDTO.UserId = vacationBalance.UserId;
            vacationBalanceDTO.VacationTypeId = vacationBalance.VacationTypeId;
            vacationBalanceDTO.VacationType = vacationBalance.VacationType?.Name;
            return vacationBalanceDTO;
        }

        public VacationBalance MapToVacationBalance(VacationBalanceDTO vacationBalanceDTO)
        {
            VacationBalance vacationBalance = new VacationBalance();
            vacationBalance.VacationBalanceId = vacationBalanceDTO.VacationBalanceId;
            vacationBalance.VacationTypeId = vacationBalanceDTO.VacationTypeId;
            vacationBalance.Balance = vacationBalanceDTO.Balance;
            vacationBalance.Used = vacationBalanceDTO.Used;
            vacationBalance.UserId = vacationBalanceDTO.UserId;
            return vacationBalance;
        }
    }
}
