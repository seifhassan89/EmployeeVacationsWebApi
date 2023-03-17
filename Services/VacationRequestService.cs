using employee_task.Mappers.Contracts;
using employee_task.Models;
using employee_task.Repositories.Contracts;
using employee_task.Services.Contracts;

namespace employee_task.Services
{
    public class VacationRequestService : IVacationRequestService
    {
        private readonly IVacationRequestRepository _vacationRequestRepository;
        private readonly IVacationRequestMapper _vacationRequestMapper;
        public VacationRequestService(IVacationRequestRepository vacationRequestRepository, IVacationRequestMapper vacationRequestMapper, IVacationTypeRepository vacationType)
        {
            _vacationRequestRepository = vacationRequestRepository;
            _vacationRequestMapper = vacationRequestMapper;
        }

        /// <summary>
        /// add vacation request
        /// </summary>
        /// <param name="vacationRequest"></param>
        /// <returns></returns>
        public VacationRequest AddVacationRequest(VacationRequestDTO vacationRequest)
        {
            VacationRequest vacationRequestEntity = _vacationRequestMapper.MapToVacationRequest(vacationRequest);
            VacationRequest vacationRequestAdded = _vacationRequestRepository.AddVacationRequest(vacationRequestEntity);
            return vacationRequestAdded;
        }

        /// <summary>
        /// delete request
        /// </summary>
        /// <param name="vacationRequestId"></param>
        /// <returns></returns>
        public bool DeleteVacationRequest(int vacationRequestId)
        {
            VacationRequest? vacationRequest = _vacationRequestRepository.GetEntityById(vacationRequestId);
            if (vacationRequest == null)
            {
                return false;
            }

            _vacationRequestRepository.DeleteVacationRequest(vacationRequest);
            return true;
        }

        /// <summary>
        /// edit request vacation data 
        /// </summary>
        /// <param name="vacationRequestId"></param>
        /// <param name="vacationRequestDTO"></param>
        public bool EditVacationRequest(int vacationRequestId, VacationRequestDTO vacationRequestDTO)
        {
            VacationRequest vacationRequestEntity = _vacationRequestMapper.MapToVacationRequest(vacationRequestDTO);
            return _vacationRequestRepository.EditVacationRequest(vacationRequestId, vacationRequestEntity);
        }

        /// <summary>
        /// get request by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VacationRequestDTO GetById(int id)
        {
            VacationRequestDTO vacationRequest = _vacationRequestRepository.GetById(id);

            return vacationRequest;
        }

        /// <summary>
        /// validate the request is still pending
        /// </summary>
        /// <param name="vacationRequest"></param>
        /// <returns></returns>
        public string validatePendingRequest(VacationRequest vacationRequest)
        {
            if (vacationRequest == null)
            {
                return "VacationRequest Not Found!!!!!!";
            }
            else if (vacationRequest.Status != null)
            {
                return "VacationRequest is not Pending!!!!!!";
            }
            return string.Empty;
        }

        /// <summary>
        /// accept request
        /// </summary>
        /// <param name="vacationRequest"></param>
        /// <returns></returns>
        public VacationRequestDTO AcceptRequest(VacationRequest vacationRequest)
        {
            vacationRequest.Status = true;
            _vacationRequestRepository.EditVacationRequest(vacationRequest.VacationRequestId, vacationRequest);
            VacationRequestDTO vacationRequestDTO = _vacationRequestMapper.MapToVacationRequestDTO(vacationRequest);
            return vacationRequestDTO;
        }
        /// <summary>
        /// reject vacation request
        /// </summary>
        /// <param name="vacationRequest"></param>
        /// <returns></returns>
        public VacationRequestDTO RejectRequest(VacationRequest vacationRequest)
        {
            vacationRequest.Status = false;
            _vacationRequestRepository.EditVacationRequest(vacationRequest.VacationRequestId, vacationRequest);
            VacationRequestDTO vacationRequestDTO = _vacationRequestMapper.MapToVacationRequestDTO(vacationRequest);
            return vacationRequestDTO;
        }
        /// <summary>
        /// get request entity by iid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VacationRequest GetByIdEntity(int id)
        {
            VacationRequest vacationRequest = _vacationRequestRepository.GetEntityById(id);
            return vacationRequest;
        }

        /// <summary>
        /// get list of vacation requests for a specific user 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<VacationRequestDTO> GetRequestsByUserId(int id)
        {
            List<VacationRequestDTO> vacationRequests = _vacationRequestRepository.GetRequestsByUserId(id);
            return vacationRequests;
        }

        /// <summary>
        /// get request list filtered and paginated
        /// </summary>
        /// <param name="listFilterDTO"></param>
        /// <returns></returns>
        public PagedResultDTO<VacationRequestDTO> VacationRequestList(ListFilterDTO listFilterDTO)
        {
            PagedResultDTO<VacationRequestDTO> pagedResultDTO = new PagedResultDTO<VacationRequestDTO>();
            List<VacationRequestDTO> list = _vacationRequestRepository.VacationRequestList(listFilterDTO);
            pagedResultDTO.List = list;
            pagedResultDTO.PageNumber = listFilterDTO.CurrentPage;
            pagedResultDTO.TotalRecords = _vacationRequestRepository.CountVacationRequest(listFilterDTO);
            return pagedResultDTO;
        }
        public void SaveChanges()
        {
            _vacationRequestRepository.SaveChanges();
        }
    }
}
