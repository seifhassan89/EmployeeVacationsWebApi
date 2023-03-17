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
    [Route("Users")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserMapper _userMapper;

        public UsersController(IUserService userService, IUserMapper userMapper)
        {
            _userService = userService;
            _userMapper = userMapper;
        }
        /// <summary>
        /// get user list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetUsers()
        {
            ResultDTO<List<UserDTO>> result = new ResultDTO<List<UserDTO>>();
            result.Results = _userService.GetAll();
            result.StatusCode = Ok().StatusCode;
            return Ok(result);
        }

        /// <summary>
        /// get user by id 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        public ActionResult GetUserById([FromRoute] int userId)
        {
            ResultDTO<UserDTO> resultDTO = new ResultDTO<UserDTO>();
            UserDTO? user = _userService.GetById(userId);
            if (user == null)
            {
                resultDTO.ErrorsMessages.Add("User Not Found!!!!!!");
                resultDTO.StatusCode = NotFound().StatusCode;
                return NotFound(resultDTO);
            }
            resultDTO.Results = user;
            resultDTO.StatusCode = Ok().StatusCode;

            return Ok(resultDTO);
        }

        /// <summary>
        /// Add user to database
        /// </summary>
        /// <param name="userDTO"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddUser([FromBody] UserDTO userDTO, [FromHeader] int RoleId)
        {
            ResultDTO<UserDTO> resultDTO = new ResultDTO<UserDTO>();
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
                resultDTO.ErrorsMessages.Add("Please Add User");
                resultDTO.StatusCode = BadRequest().StatusCode;
                return BadRequest(resultDTO);
            }
            else
            {
                try
                {
                    User AddUser = _userService.AddUser(userDTO);
                    _userService.SaveChanges();
                    UserDTO userDTOResult = _userMapper.MapToUserDTO(AddUser);
                    resultDTO.Results = userDTOResult;
                    resultDTO.StatusCode = Ok().StatusCode;
                    resultDTO.Messages.Add("User Added Successfully");
                    return Ok(resultDTO);
                }
                catch (Exception e)
                {
                    return BadRequest(e);
                }
            }
        }

        /// <summary>
        /// edit user data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userDTO"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        [HttpPut("{userId}")]
        public ActionResult EditUser([FromRoute] int userId, [FromBody] UserDTO userDTO, [FromHeader] int RoleId)
        {
            ResultDTO<UserDTO> resultDTO = new ResultDTO<UserDTO>();
            if (RoleId != (int)RoleEnum.HR_Manger)
            {
                resultDTO.ErrorsMessages = new List<string>();
                resultDTO.ErrorsMessages.Add("This role can't proceed this action");
                resultDTO.StatusCode = StatusCodes.Status403Forbidden;
                return BadRequest(resultDTO);
            }
            if (!ModelState.IsValid || userDTO.UserId != userId)
            {
                resultDTO.ErrorsMessages = new List<string>();
                resultDTO.ErrorsMessages.Add("Please Add valid user");
                resultDTO.StatusCode = BadRequest().StatusCode;
                return BadRequest(resultDTO);
            }
            else
            {
                bool isEdited = _userService.EditUser(userId, userDTO);

                if (!isEdited)
                {
                    resultDTO.ErrorsMessages.Add("User Not Found!!!!!!");
                    resultDTO.StatusCode = NotFound().StatusCode;
                    return NotFound(resultDTO);
                }
                _userService.EditUser(userId, userDTO);
                _userService.SaveChanges();
                resultDTO.StatusCode = Ok().StatusCode;
                resultDTO.Results = userDTO;
                resultDTO.Messages.Add("User Updated Successfully");
                return Ok(resultDTO);
            }
        }

        /// <summary>
        /// delete user by id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        [HttpDelete("{userId}")]
        public ActionResult DeleteUser([FromRoute] int userId, [FromHeader] int RoleId)
        {
            ResultDTO<UserDTO> resultDTO = new ResultDTO<UserDTO>();
            if (RoleId != (int)RoleEnum.HR_Manger)
            {
                resultDTO.ErrorsMessages = new List<string>();
                resultDTO.ErrorsMessages.Add("This role can't proceed this action");
                resultDTO.StatusCode = StatusCodes.Status403Forbidden;
                return BadRequest(resultDTO);
            }
            bool isDeleted = _userService.DeleteUser(userId);

            if (!isDeleted)
            {
                resultDTO.ErrorsMessages.Add("User Not Found!!!!!!");
                resultDTO.StatusCode = NotFound().StatusCode;
                return NotFound(resultDTO);
            }
            _userService.SaveChanges();
            resultDTO.StatusCode = Ok().StatusCode;
            resultDTO.Messages.Add("User Deleted Successfully");
            return Ok(resultDTO);
        }
    }
}
