using employee_task.Data;
using employee_task.Models;

namespace employee_task.Repositories.Contracts
{
    public interface IVacationRequestRepository : IBaseRepository
    {

        public VacationRequestDTO GetById(int id);
        public VacationRequest GetEntityById(int id);
        public List<VacationRequestDTO> GetRequestsByUserId(int id);
        public VacationRequest AddVacationRequest(VacationRequest vacationRequest);
        public bool EditVacationRequest(int vacationRequestId, VacationRequest vacationRequest);
        public void DeleteVacationRequest(VacationRequest vacationRequest);
        public List<VacationRequestDTO> VacationRequestList(ListFilterDTO listFilterDTO);
        public int CountVacationRequest(ListFilterDTO listFilterDTO);
    }
}
