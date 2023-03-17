using employee_task.Models;

namespace employee_task.Mappers.Contracts
{
    public interface IVacationRequestMapper
    {
        public VacationRequest MapToVacationRequest(VacationRequestDTO vacationRequestDTO);
        public VacationRequestDTO MapToVacationRequestDTO(VacationRequest vacationRequest);

    }
}
