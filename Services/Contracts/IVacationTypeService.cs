using employee_task.Models;

namespace employee_task.Services.Contracts
{
    public interface IVacationTypeService
    {
        public List<VacationTypeDTO> GetAll();
        public VacationTypeDTO? GetById(int id);
        public VacationType AddVacationType(VacationTypeDTO vacationType);
        public bool EditVacationType(int vacationTypeId, VacationTypeDTO vacationTypeDTO);
        public bool DeleteVacationType(int vacationTypeId);
        public void SaveChanges();
    }
}
