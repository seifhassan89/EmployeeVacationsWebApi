using employee_task.Mappers.Contracts;
using employee_task.Models;
using employee_task.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Models.Enum;

namespace employee_task.Controllers
{
    [ApiController]
    [Route("VacationRequests")]
    public class VacationRequestController : Controller
    {

        private readonly IVacationRequestService _vacationRequestService;
        private readonly IVacationRequestMapper _vacationRequestMapper;
        private readonly IUserService _userService;
        private readonly IVacationBalanceService _vacationBalanceService;

        public VacationRequestController(
            IVacationRequestService vacationRequestService,
            IVacationRequestMapper vacationRequestMapper,
            IUserService userService,
            IVacationBalanceService vacationBalanceService)
        {

            _vacationRequestService = vacationRequestService;
            _vacationRequestMapper = vacationRequestMapper;
            _userService = userService;
            _vacationBalanceService = vacationBalanceService;
        }

        /// <summary>
        /// Add request vacation to DB
        /// </summary>
        /// <param name="vacationRequestDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddVacationRequest([FromBody] VacationRequestDTO vacationRequestDTO)
        {
            ResultDTO<VacationRequestDTO> resultDTO = new ResultDTO<VacationRequestDTO>();
            if (!ModelState.IsValid)
            {
                resultDTO.ErrorsMessages = new List<string>();
                resultDTO.ErrorsMessages.Add("Please Add valid VacationRequest");
                resultDTO.StatusCode = BadRequest().StatusCode;
                return BadRequest(resultDTO);
            }
            else
            {
                try
                {
                    VacationRequest AddVacationRequest = _vacationRequestService.AddVacationRequest(vacationRequestDTO);
                    _vacationRequestService.SaveChanges();
                    VacationRequestDTO vacationRequestDTOResult = _vacationRequestMapper.MapToVacationRequestDTO(AddVacationRequest);
                    resultDTO.Results = vacationRequestDTOResult;
                    resultDTO.StatusCode = Ok().StatusCode;
                    resultDTO.Messages.Add("Request Vacation Added Successfully");
                    return Ok(resultDTO);
                }
                catch (Exception e)
                {
                    return BadRequest(e);
                }
            }
        }

        /// <summary>
        /// edit vacationRequest data
        /// </summary>
        /// <param name="vacationRequestId"></param>
        /// <param name="vacationRequestDTO"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        [HttpPut("{vacationRequestId}")]
        public ActionResult EditVacationRequest([FromRoute] int vacationRequestId, [FromBody] VacationRequestDTO vacationRequestDTO, [FromHeader] int RoleId)
        {
            ResultDTO<VacationRequestDTO> resultDTO = new ResultDTO<VacationRequestDTO>();
            if (RoleId != (int)RoleEnum.HR_Manger)
            {
                resultDTO.ErrorsMessages = new List<string>();
                resultDTO.ErrorsMessages.Add("This role can't proceed this action");
                resultDTO.StatusCode = StatusCodes.Status403Forbidden;
                return BadRequest(resultDTO);
            }
            if (!ModelState.IsValid || vacationRequestDTO.VacationRequestId != vacationRequestId)
            {
                resultDTO.ErrorsMessages = new List<string>();
                resultDTO.ErrorsMessages.Add("Please Add valid Vacation Request");
                resultDTO.StatusCode = BadRequest().StatusCode;
                return BadRequest(resultDTO);
            }
            else
            {
                bool isEdited = _vacationRequestService.EditVacationRequest(vacationRequestId, vacationRequestDTO);

                if (!isEdited)
                {
                    resultDTO.ErrorsMessages.Add("VacationRequest Not Found!!!!!!");
                    resultDTO.StatusCode = NotFound().StatusCode;
                    return NotFound(resultDTO);
                }
                _vacationRequestService.EditVacationRequest(vacationRequestId, vacationRequestDTO);
                _vacationRequestService.SaveChanges();
                resultDTO.StatusCode = Ok().StatusCode;
                resultDTO.Results = vacationRequestDTO;
                resultDTO.Messages.Add("VacationRequest Updated Successfully");
                return Ok(resultDTO);
            }
        }

        /// <summary>
        /// delete vacationRequest by id
        /// </summary>
        /// <param name="vacationRequestId"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        [HttpDelete("{vacationRequestId}")]
        public ActionResult DeleteVacationRequest([FromRoute] int vacationRequestId, [FromHeader] int RoleId)
        {
            ResultDTO<VacationRequestDTO> resultDTO = new ResultDTO<VacationRequestDTO>();
            if (RoleId != (int)RoleEnum.HR_Manger)
            {
                resultDTO.ErrorsMessages = new List<string>();
                resultDTO.ErrorsMessages.Add("This role can't proceed this action");
                resultDTO.StatusCode = StatusCodes.Status403Forbidden;
                return BadRequest(resultDTO);
            }
            bool isDeleted = _vacationRequestService.DeleteVacationRequest(vacationRequestId);

            if (!isDeleted)
            {
                resultDTO.ErrorsMessages.Add("Vacation Request Not Found!!!!!!");
                resultDTO.StatusCode = NotFound().StatusCode;
                return NotFound(resultDTO);
            }
            _vacationRequestService.SaveChanges();
            resultDTO.StatusCode = Ok().StatusCode;
            resultDTO.Messages.Add("VacationRequest Deleted Successfully");
            return Ok(resultDTO);
        }
        /// <summary>
        /// get request by id
        /// </summary>
        /// <param name="vacationRequestId"></param>
        /// <returns></returns>
        [HttpGet("{vacationRequestId}")]
        public ActionResult GetVacationRequestById([FromRoute] int vacationRequestId)
        {
            ResultDTO<VacationRequestDTO> resultDTO = new ResultDTO<VacationRequestDTO>();
            VacationRequestDTO vacationRequest = _vacationRequestService.GetById(vacationRequestId);
            if (vacationRequest == null)
            {
                resultDTO.ErrorsMessages.Add("Vacation Request Not Found!!!!!!");
                resultDTO.StatusCode = NotFound().StatusCode;
                return NotFound(resultDTO);
            }
            resultDTO.Results = vacationRequest;
            resultDTO.StatusCode = Ok().StatusCode;

            return Ok(resultDTO);
        }

        /// <summary>
        /// get requests list for a specific user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("userRequests/{userId}")]
        public ActionResult GetVacationRequestByUserId([FromRoute] int userId)
        {
            ResultDTO<List<VacationRequestDTO>> resultDTO = new ResultDTO<List<VacationRequestDTO>>();

            UserDTO? user = _userService.GetById(userId);
            if (user == null)
            {
                resultDTO.ErrorsMessages.Add("User Not Found!!!!!!");
                resultDTO.StatusCode = NotFound().StatusCode;
                return NotFound(resultDTO);
            }
            else
            {
                List<VacationRequestDTO> vacationRequests = _vacationRequestService.GetRequestsByUserId(userId);
                resultDTO.Results = vacationRequests;
                resultDTO.StatusCode = Ok().StatusCode;

                return Ok(resultDTO);
            }
        }
        /// <summary>
        /// accept request 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        [HttpPut("vacationRequests/accept/{Id}")]
        public ActionResult AcceptVacationRequestById([FromRoute] int Id, [FromHeader] int RoleId)
        {
            ResultDTO<VacationRequestDTO> resultDTO = new ResultDTO<VacationRequestDTO>();
            if (RoleId != (int)RoleEnum.HR_Manger)
            {
                resultDTO.ErrorsMessages = new List<string>();
                resultDTO.ErrorsMessages.Add("This role can't proceed this action");
                resultDTO.StatusCode = StatusCodes.Status403Forbidden;
                return BadRequest(resultDTO);
            }
            VacationRequest vacationRequest = _vacationRequestService.GetByIdEntity(Id);
            string errorMessage = _vacationRequestService.validatePendingRequest(vacationRequest);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                resultDTO.ErrorsMessages.Add(errorMessage);
                resultDTO.StatusCode = BadRequest().StatusCode;
                return BadRequest(resultDTO);
            }
            VacationRequestDTO vacationRequesDTO = _vacationRequestService.AcceptRequest(vacationRequest);
            bool isUpdated = _vacationBalanceService.UpdateBalance(vacationRequest);
            _vacationRequestService.SaveChanges();
            resultDTO.Results = vacationRequesDTO;
            resultDTO.StatusCode = Ok().StatusCode;
            resultDTO.Messages.Add("Vacation Request Accepted Successfully");
            return Ok(resultDTO);
        }
        /// <summary>
        /// reject request
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        [HttpPut("vacationRequests/reject/{Id}")]
        public ActionResult RejectVacationRequestById([FromRoute] int Id, [FromHeader] int RoleId)
        {
            ResultDTO<VacationRequestDTO> resultDTO = new ResultDTO<VacationRequestDTO>();
            if (RoleId != (int)RoleEnum.HR_Manger)
            {
                resultDTO.ErrorsMessages = new List<string>();
                resultDTO.ErrorsMessages.Add("This role can't proceed this action");
                resultDTO.StatusCode = StatusCodes.Status403Forbidden;
                return BadRequest(resultDTO);
            }
            VacationRequest vacationRequest = _vacationRequestService.GetByIdEntity(Id);
            string ErrorMessage = _vacationRequestService.validatePendingRequest(vacationRequest);
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                resultDTO.ErrorsMessages.Add(ErrorMessage);
                resultDTO.StatusCode = BadRequest().StatusCode;
                return BadRequest(resultDTO);
            }
            VacationRequestDTO vacationRequestDTO = _vacationRequestService.RejectRequest(vacationRequest);
            _vacationRequestService.SaveChanges();
            resultDTO.Results = vacationRequestDTO;
            resultDTO.StatusCode = Ok().StatusCode;
            resultDTO.Messages.Add("Vacation Request Rejected Successfully");
            return Ok(resultDTO);
        }

        [HttpPost("getFilteredList")]
        public ActionResult VacationsRequestList([FromBody] ListFilterDTO listFilterDTO)
        {
            PagedResultDTO<VacationRequestDTO> resultDTO = _vacationRequestService.VacationRequestList(listFilterDTO);
            return Ok(resultDTO);

        }

    }
}
