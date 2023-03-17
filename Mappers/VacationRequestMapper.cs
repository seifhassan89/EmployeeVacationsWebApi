using employee_task.Mappers.Contracts;
using employee_task.Models;

namespace employee_task.Mappers
{
    public class VacationRequestMapper : IVacationRequestMapper
    {
        public VacationRequestDTO MapToVacationRequestDTO(VacationRequest vacationRequest)
        {
            VacationRequestDTO vacationRequestDTO = new VacationRequestDTO();

            vacationRequestDTO.VacationRequestId = vacationRequest.VacationRequestId;
            vacationRequestDTO.UserId = vacationRequest.UserId;
            vacationRequestDTO.Status = vacationRequest.Status;
            vacationRequestDTO.VacationTypeId = vacationRequest.VacationTypeId;
            vacationRequestDTO.UserFullName = vacationRequest.User != null? vacationRequest.User.FullName:"";
            vacationRequestDTO.VacationType = vacationRequest.VacationType != null ? vacationRequest.VacationType.Name : "";
            vacationRequestDTO.StartDate = vacationRequest.StartDate;
            vacationRequestDTO.EndDate = vacationRequest.EndDate;
            return vacationRequestDTO;
        }

        public VacationRequest MapToVacationRequest(VacationRequestDTO vacationRequestDTO)
        {
            VacationRequest vacationRequest = new VacationRequest();

            vacationRequest.VacationRequestId = vacationRequestDTO.VacationRequestId;
            vacationRequest.VacationTypeId = vacationRequestDTO.VacationTypeId;
            vacationRequest.UserId =vacationRequestDTO.UserId;
            vacationRequest.Status = vacationRequestDTO.Status;
            vacationRequest.StartDate = vacationRequestDTO.StartDate;
            vacationRequest.EndDate = vacationRequestDTO.EndDate;
            return vacationRequest;
        }
    }
}
