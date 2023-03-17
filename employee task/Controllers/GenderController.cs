using employee_task.Mappers;
using employee_task.Mappers.Contracts;
using employee_task.Models;
using employee_task.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;

namespace employee_task.Controllers
{
    [ApiController]
    [Route("Gender")]
    public class GenderController : Controller
    {
        private readonly IGenderService _genderService;
        private readonly IGenderMapper _genderMapper;

        public GenderController(IGenderService genderService, IGenderMapper genderMapper)
        {
            _genderService = genderService;
            _genderMapper = genderMapper;
        }



        /// <summary>
        /// get gender list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetGenders()
        {
            ResultDTO<List<GenderDTO>> result = new ResultDTO<List<GenderDTO>>();
            result.Results = _genderService.GetAll();
            result.StatusCode = Ok().StatusCode;
            return Ok(result);
        }

        /// <summary>
        /// get gender by id
        /// </summary>
        /// <param name="genderId"></param>
        /// <returns></returns>
        [HttpGet("{genderId}")]
        public ActionResult GetGenderById([FromRoute] int genderId)
        {
            ResultDTO<GenderDTO> resultDTO = new ResultDTO<GenderDTO>();
            GenderDTO? gender = _genderService.GetById(genderId);
            if (gender == null)
            {
                resultDTO.ErrorsMessages.Add("Gender Not Found!!!!!!");
                resultDTO.StatusCode = NotFound().StatusCode;
                return NotFound(resultDTO);
            }
            resultDTO.Results = gender;
            resultDTO.StatusCode = Ok().StatusCode;

            return Ok(resultDTO);
        }

        /// <summary>to Db
        /// add gender 
        /// </summary>
        /// <param name="genderDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddGender([FromBody] GenderDTO genderDTO)
        {
            ResultDTO<GenderDTO> resultDTO = new ResultDTO<GenderDTO>();
            if (!ModelState.IsValid)
            {
                resultDTO.ErrorsMessages = new List<string>();
                resultDTO.ErrorsMessages.Add("Please Add Gender");
                resultDTO.StatusCode = BadRequest().StatusCode;
                return BadRequest(resultDTO);
            }
            else
            {
                try
                {
                    Gender AddGender = _genderService.AddGender(genderDTO);
                    _genderService.SaveChanges();
                    GenderDTO genderDTOResult = _genderMapper.MapToGenderDTO(AddGender);
                    resultDTO.Results = genderDTOResult;
                    resultDTO.StatusCode = Ok().StatusCode;
                    return Ok(resultDTO);

                }
                catch (Exception e)
                {
                    return BadRequest(e);
                }
            }
        }


        /// <summary>
        /// edit gender data in db
        /// </summary>
        /// <param name="genderId"></param>
        /// <param name="genderDTO"></param>
        /// <returns></returns>
        [HttpPut("{genderId}")]
        public ActionResult EditGender([FromRoute] int genderId, [FromBody] GenderDTO genderDTO)
        {
            ResultDTO<GenderDTO> resultDTO = new ResultDTO<GenderDTO>();
            if (!ModelState.IsValid || genderDTO.GenderId != genderId)
            {
                resultDTO.ErrorsMessages = new List<string>();
                resultDTO.ErrorsMessages.Add("Please Add valid Gender");
                resultDTO.StatusCode = BadRequest().StatusCode;
                return BadRequest(resultDTO);
            }
            else
            {
                bool isEdited = _genderService.EditGender(genderId, genderDTO);
                if (!isEdited)
                {
                    resultDTO.ErrorsMessages.Add("Gender Not Found!!!!!!");
                    resultDTO.StatusCode = NotFound().StatusCode;
                    return NotFound(resultDTO);

                }

                _genderService.SaveChanges();
                resultDTO.StatusCode = Ok().StatusCode;
                resultDTO.Results = genderDTO;
                return Ok(resultDTO);
            }
        }

        /// <summary>
        /// delete gender from db
        /// </summary>
        /// <param name="genderId"></param>
        /// <returns></returns>

        [HttpDelete("{genderId}")]
        public ActionResult DeleteGender([FromRoute] int genderId)
        {
            ResultDTO<GenderDTO> resultDTO = new ResultDTO<GenderDTO>();
            bool isDeleted = _genderService.DeleteGender(genderId);
            if (!isDeleted)
            {
                resultDTO.ErrorsMessages.Add("Gender Not Found!!!!!!");
                resultDTO.StatusCode = NotFound().StatusCode;
                return NotFound(resultDTO);

            }
            _genderService.SaveChanges();
            resultDTO.StatusCode = Ok().StatusCode;
            resultDTO.Messages.Add("Gender Deleted Successfully");
            return Ok(resultDTO);

        }
    }
}
