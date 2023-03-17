using employee_task.Models;

namespace employee_task.Mappers.Contracts
{
    public interface IVacationTypeMapper
    {
        public VacationType MapToVacationType(VacationTypeDTO vecationTypeDTO);
        public VacationTypeDTO MapToVacationTypeDTO(VacationType vecationType);

    }
}
