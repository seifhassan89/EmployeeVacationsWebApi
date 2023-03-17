using employee_task.Mappers;
using employee_task.Mappers.Contracts;
using employee_task.Models;
using employee_task.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Models.Enum;
using System.Collections.Generic;

namespace employee_task.Controllers
{
    [ApiController]
    [Route("VacationType")]
    public class VacationTypeController : Controller
    {
        private readonly IVacationTypeService _vacationTypeService;
        private readonly IVacationTypeMapper _vacationTypeMapper;

        public VacationTypeController(IVacationTypeService vacationTypeService, IVacationTypeMapper vacationTypeMapper)
        {
            _vacationTypeService = vacationTypeService;
            _vacationTypeMapper = vacationTypeMapper;
        }

        /// <summary>
        /// get vacation type list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetVacationType()
        {
            ResultDTO<List<VacationTypeDTO>> result = new ResultDTO<List<VacationTypeDTO>>();

            result.Results = _vacationTypeService.GetAll();
            result.StatusCode = Ok().StatusCode;
            return Ok(result);
        }

        /// <summary>
        /// get vacation type by id
        /// </summary>
        /// <param name="vacationTypeId"></param>
        /// <returns></returns>
        [HttpGet("{vacationTypeId}")]
        public ActionResult GetVacationTypeById([FromRoute] int vacationTypeId)
        {
            ResultDTO<VacationTypeDTO> resultDTO = new ResultDTO<VacationTypeDTO>();

            VacationTypeDTO? vacationType = _vacationTypeService.GetById(vacationTypeId);
            if (vacationType == null)
            {
                resultDTO.ErrorsMessages.Add("VacationType Not Found!!!!!!");
                resultDTO.StatusCode = NotFound().StatusCode;
                return NotFound(resultDTO);
            }
            resultDTO.Results = vacationType;
            resultDTO.StatusCode = Ok().StatusCode;

            return Ok(resultDTO);
        }

        /// <summary>
        /// HR Manger add vacation type 
        /// </summary>
        /// <param name="vacationTypeDTO"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddVacationType([FromBody] VacationTypeDTO vacationTypeDTO, [FromHeader] int RoleId)
        {
            ResultDTO<VacationTypeDTO> resultDTO = new ResultDTO<VacationTypeDTO>();
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
                resultDTO.ErrorsMessages.Add("Please Add valid VacationType");
                resultDTO.StatusCode = BadRequest().StatusCode;
                return BadRequest(resultDTO);
            }
            else
            {
                try
                {
                    VacationType AddVacationType = _vacationTypeService.AddVacationType(vacationTypeDTO);
                    _vacationTypeService.SaveChanges();
                    VacationTypeDTO vacationTypeDTOResult = _vacationTypeMapper.MapToVacationTypeDTO(AddVacationType);
                    resultDTO.Results = vacationTypeDTOResult;
                    resultDTO.StatusCode = Ok().StatusCode;
                    resultDTO.Messages.Add("VacationType added Successfully");
                    return Ok(resultDTO);
                }
                catch (Exception e)
                {
                    return BadRequest(e);
                }
            }
        }

        /// <summary>
        /// edit vacation type
        /// </summary>
        /// <param name="vacationTypeId"></param>
        /// <param name="vacationTypeDTO"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        [HttpPut("{vacationTypeId}")]
        public ActionResult EditVacationType([FromRoute] int vacationTypeId, [FromBody] VacationTypeDTO vacationTypeDTO, [FromHeader] int RoleId)
        {
            ResultDTO<VacationTypeDTO> resultDTO = new ResultDTO<VacationTypeDTO>();
            if (RoleId != (int)RoleEnum.HR_Manger)
            {
                resultDTO.ErrorsMessages = new List<string>();
                resultDTO.ErrorsMessages.Add("This role can't proceed this action");
                resultDTO.StatusCode = StatusCodes.Status403Forbidden;
                return BadRequest(resultDTO);
            }
            if (!ModelState.IsValid || vacationTypeDTO.VacationTypeId != vacationTypeId)
            {
                resultDTO.ErrorsMessages = new List<string>();
                resultDTO.ErrorsMessages.Add("Please Add valid Vacation Type");
                resultDTO.StatusCode = BadRequest().StatusCode;
                return BadRequest(resultDTO);
            }
            else
            {
                bool isEdited = _vacationTypeService.EditVacationType(vacationTypeId, vacationTypeDTO);
                if (!isEdited)
                {
                    resultDTO.ErrorsMessages.Add("Vacation Type Not Found!!!!!!");
                    resultDTO.StatusCode = NotFound().StatusCode;
                    return NotFound(resultDTO);
                }
                _vacationTypeService.SaveChanges();
                resultDTO.StatusCode = Ok().StatusCode;
                resultDTO.Results = vacationTypeDTO;
                resultDTO.Messages.Add("Vacation Type updated Successfully");

                return Ok(resultDTO);
            }
        }

        /// <summary>
        /// delete vacation type by id
        /// </summary>
        /// <param name="vacationTypeId"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        [HttpDelete("{vacationTypeId}")]
        public ActionResult DeleteVacationType([FromRoute] int vacationTypeId, [FromHeader] int RoleId)
        {
            ResultDTO<VacationTypeDTO> resultDTO = new ResultDTO<VacationTypeDTO>();
            if (RoleId != (int)RoleEnum.HR_Manger)
            {
                resultDTO.ErrorsMessages = new List<string>();
                resultDTO.ErrorsMessages.Add("This role can't proceed this action");
                resultDTO.StatusCode = StatusCodes.Status403Forbidden;
                return BadRequest(resultDTO);
            }
            bool isDeleted = _vacationTypeService.DeleteVacationType(vacationTypeId);
            if (!isDeleted)
            {
                resultDTO.ErrorsMessages.Add("VacationType Not Found!!!!!!");
                resultDTO.StatusCode = NotFound().StatusCode;
                return NotFound(resultDTO);
            }
            _vacationTypeService.SaveChanges();
            resultDTO.StatusCode = Ok().StatusCode;
            resultDTO.Messages.Add("Vacation Type Deleted Successfully");
            return Ok(resultDTO);
        }
    }
}
