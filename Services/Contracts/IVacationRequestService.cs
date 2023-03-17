using employee_task.Models;

namespace employee_task.Services.Contracts
{
    public interface IVacationRequestService
    {
        public VacationRequest AddVacationRequest(VacationRequestDTO vacationRequest);
        public VacationRequestDTO GetById(int id);
        public VacationRequestDTO AcceptRequest(VacationRequest vacationRequest);
        public VacationRequestDTO RejectRequest(VacationRequest vacationRequest);
        public string validatePendingRequest(VacationRequest vacationRequest);
        public VacationRequest GetByIdEntity(int id);
        public List<VacationRequestDTO> GetRequestsByUserId(int id);
        public bool EditVacationRequest(int vacationRequestId, VacationRequestDTO vacationRequestDTO);
        public bool DeleteVacationRequest(int vacationRequestId);
        public PagedResultDTO<VacationRequestDTO> VacationRequestList(ListFilterDTO listFilterDTO);
        public void SaveChanges();
    }
}
