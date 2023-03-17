using employee_task.Mappers.Contracts;
using employee_task.Models;

namespace employee_task.Mappers
{
    public class VacationTypeMapper : IVacationTypeMapper
    {
        public VacationTypeDTO MapToVacationTypeDTO(VacationType vacationType)
        {
            VacationTypeDTO vacationTypeDTO = new VacationTypeDTO();
            vacationTypeDTO.IntialValue = vacationType.IntialValue;
            vacationTypeDTO.VacationTypeId = (int)vacationType.VacationTypeId;
            vacationTypeDTO.Name = vacationType.Name;
            return vacationTypeDTO;
        }

        public VacationType MapToVacationType(VacationTypeDTO vacationTypeDTO)
        {
            VacationType vacationType = new VacationType();
            vacationType.IntialValue = vacationTypeDTO.IntialValue;
            vacationType.VacationTypeId = vacationTypeDTO.VacationTypeId;
            vacationType.Name = vacationTypeDTO.Name;
            return vacationType;
        }
    }
}
