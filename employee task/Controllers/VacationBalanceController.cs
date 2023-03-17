using employee_task.Mappers.Contracts;
using employee_task.Models;
using employee_task.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Models.Enum;

namespace employee_task.Controllers
{
    [ApiController]
    [Route("VacationBalance")]
    public class VacationBalanceController : Controller
    {

        private readonly IVacationBalanceService _vacationBalanceService;
        private readonly IVacationBalanceMapper _vacationBalanceMapper;
        private readonly IVacationBalanceService _userService;

        public VacationBalanceController(IVacationBalanceService userService, IVacationBalanceService vacationBalanceService, IVacationBalanceMapper vacationBalanceMapper)
        {
            _vacationBalanceService = vacationBalanceService;
            _vacationBalanceMapper = vacationBalanceMapper;
            _userService = userService;
        }

        /// <summary>
        /// update vacation balance Data
        /// </summary>
        /// <param name="vacationBalanceId"></param>
        /// <param name="roleId"></param>
        /// <param name="vacationBalanceBody"></param>
        /// <returns></returns>
        [HttpPut("{vacationBalanceId}")]
        public ActionResult EditVacationBlanceByVacationBalanceId([FromRoute] int vacationBalanceId, [FromHeader] int roleId, [FromBody] VacationBalanceDTO vacationBalanceBody)
        {
            ResultDTO<VacationBalanceDTO> resultDTO = new ResultDTO<VacationBalanceDTO>();
            if (roleId != (int)RoleEnum.HR_Manger)
            {
                resultDTO.ErrorsMessages = new List<string>();
                resultDTO.ErrorsMessages.Add("This role can't proceed this action");
                resultDTO.StatusCode = StatusCodes.Status403Forbidden;
                return BadRequest(resultDTO);
            }
            if (!ModelState.IsValid)
            {
                resultDTO.ErrorsMessages = new List<string>();
                resultDTO.ErrorsMessages.Add("Please Add valid vacation balance");
                resultDTO.StatusCode = BadRequest().StatusCode;
                return BadRequest(resultDTO);
            }

            _vacationBalanceService.EditVacationBalaceByVacationBalanceId(vacationBalanceId, vacationBalanceBody);
            _vacationBalanceService.SaveChanges();
            resultDTO.Results = vacationBalanceBody;
            resultDTO.Messages.Add("Vacation Balance updated Successfully");
            return Ok(resultDTO);
        }

        /// <summary>
        /// get vacation Balance by id 
        /// </summary>
        /// <param name="vacationBalanceId"></param>
        /// <returns></returns>
        [HttpGet("{vacationBalanceId}")]
        public ActionResult GetVacationBalanceById([FromRoute] int vacationBalanceId)
        {
            ResultDTO<VacationBalanceDTO> resultDTO = new ResultDTO<VacationBalanceDTO>();
            VacationBalanceDTO? vacationBalance = _vacationBalanceService.GetById(vacationBalanceId);
            if (vacationBalance == null)
            {
                resultDTO.ErrorsMessages.Add("VacationBalance Not Found!!!!!!");
                resultDTO.StatusCode = NotFound().StatusCode;
                return NotFound(resultDTO);
            }
            resultDTO.Results = vacationBalance;
            resultDTO.StatusCode = Ok().StatusCode;

            return Ok(resultDTO);
        }

        /// <summary>
        /// get vacation Balance list for a specific user
        /// </summary>
        /// <param name="vacationBalanceId"></param>
        /// <returns></returns>
        [HttpGet("GetUseerVacationBalances/{userID}")]
        public ActionResult GetVacationBalanceByUserId([FromRoute] int userID)
        {
            ResultDTO<List<VacationBalanceDTO>> resultDTO = new ResultDTO<List<VacationBalanceDTO>>();
            List<VacationBalanceDTO> vacationBalances = _vacationBalanceService.GetAllByUserID(userID);
            resultDTO.Results = vacationBalances;
            resultDTO.StatusCode = Ok().StatusCode;
            return Ok(resultDTO);
        }

        /// <summary>
        /// Add vacationBalance to database
        /// </summary>
        /// <param name="vacationBalanceDTO"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddVacationBalance([FromBody] VacationBalanceDTO vacationBalanceDTO, [FromHeader] int RoleId)
        {
            ResultDTO<VacationBalanceDTO> resultDTO = new ResultDTO<VacationBalanceDTO>();
            if (RoleId != (int)RoleEnum.HR_Manger)
            {
                resultDTO.ErrorsMessages = new List<string>();
                resultDTO.ErrorsMessages.Add("This role can't proceed this action");
                resultDTO.StatusCode = StatusCodes.Status403Forbidden;
                return BadRequest(resultDTO);
            }
            if (!ModelState.IsValid)
            {
                resultDTO.ErrorsMessages = new List<string>();
                resultDTO.ErrorsMessages.Add("Please Add VacationBalance");
                resultDTO.StatusCode = BadRequest().StatusCode;
                return BadRequest(resultDTO);
            }
            else
            {
                try
                {
                    VacationBalance AddVacationBalance = _vacationBalanceService.AddVacationBalance(vacationBalanceDTO);
                    _vacationBalanceService.SaveChanges();
                    VacationBalanceDTO vacationBalanceDTOResult = _vacationBalanceMapper.MapToVacationBalanceDTO(AddVacationBalance);
                    resultDTO.Results = vacationBalanceDTOResult;
                    resultDTO.StatusCode = Ok().StatusCode;
                    resultDTO.Messages.Add("VacationBalance Added Successfully");
                    return Ok(resultDTO);
                }
                catch (Exception e)
                {
                    return BadRequest(e);
                }
            }
        }

        /// <summary>
        /// delete vacationBalance by id
        /// </summary>
        /// <param name="vacationBalanceId"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        [HttpDelete("{vacationBalanceId}")]
        public ActionResult DeleteVacationBalance([FromRoute] int vacationBalanceId, [FromHeader] int RoleId)
        {
            ResultDTO<VacationBalanceDTO> resultDTO = new ResultDTO<VacationBalanceDTO>();
            if (RoleId != (int)RoleEnum.HR_Manger)
            {
                resultDTO.ErrorsMessages = new List<string>();
                resultDTO.ErrorsMessages.Add("This role can't proceed this action");
                resultDTO.StatusCode = StatusCodes.Status403Forbidden;
                return BadRequest(resultDTO);
            }
            bool isDeleted = _vacationBalanceService.DeleteVacationBalance(vacationBalanceId);

            if (!isDeleted)
            {
                resultDTO.ErrorsMessages.Add("VacationBalance Not Found!!!!!!");
                resultDTO.StatusCode = NotFound().StatusCode;
                return NotFound(resultDTO);
            }
            _vacationBalanceService.SaveChanges();
            resultDTO.StatusCode = Ok().StatusCode;
            resultDTO.Messages.Add("VacationBalance Deleted Successfully");
            return Ok(resultDTO);
        }


    }
}
