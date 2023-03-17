using employee_task.Data;
using employee_task.Models;

namespace employee_task.Repositories.Contracts
{
    public interface IVacationTypeRepository : IBaseRepository
    {
        public List<VacationType> GetAll();
        public VacationType? GetById(int id);
        public VacationType AddVacationType(VacationType vecationType);
        bool EditVacationType(int vecationTypeId, VacationType vecationType);
        public void DeleteVacationType(VacationType vecationType);
    }
}
