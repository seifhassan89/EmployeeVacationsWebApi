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
    [Route("Role")]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly IRoleMapper _roleMapper;

        public RoleController(IRoleService roleService, IRoleMapper roleMapper)
        {
            _roleService = roleService;
            _roleMapper = roleMapper;
        }

        /// <summary>
        /// get role list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetRoles()
        {
            ResultDTO<List<RoleDTO>> result = new ResultDTO<List<RoleDTO>>();

            result.Results = _roleService.GetAll();

            result.StatusCode = Ok().StatusCode;
            return Ok(result);
        }

        /// <summary>
        /// get role by id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet("{roleId}")]
        public ActionResult GetRoleById([FromRoute] int roleId)
        {
            ResultDTO<RoleDTO> resultDTO = new ResultDTO<RoleDTO>();
            RoleDTO? role = _roleService.GetById(roleId);
            if (role == null)
            {
                resultDTO.ErrorsMessages.Add("Role Not Found!!!!!!");
                resultDTO.StatusCode = NotFound().StatusCode;
                return NotFound(resultDTO);
            }
            resultDTO.Results = role;
            resultDTO.StatusCode = Ok().StatusCode;

            return Ok(resultDTO);
        }


        /// <summary>
        /// add role to Db
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddRole([FromBody] RoleDTO roleDTO)
        {
            ResultDTO<RoleDTO> resultDTO = new ResultDTO<RoleDTO>();
            if (!ModelState.IsValid)
            {
                resultDTO.ErrorsMessages = new List<string>();
                resultDTO.ErrorsMessages.Add("Please Add Role");
                resultDTO.StatusCode = BadRequest().StatusCode;
                return BadRequest(resultDTO);
            }
            else
            {
                try
                {
                    Role AddRole = _roleService.AddRole(roleDTO);
                    _roleService.SaveChanges();
                    RoleDTO roleDTOResult = _roleMapper.MapToRoleDTO(AddRole);
                    resultDTO.Results = roleDTOResult;
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
        /// edit role data in db
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="roleDTO"></param>
        /// <returns></returns>
        [HttpPut("{roleId}")]
        public ActionResult EditRole([FromRoute] int roleId, [FromBody] RoleDTO roleDTO)
        {
            ResultDTO<RoleDTO> resultDTO = new ResultDTO<RoleDTO>();
            if (!ModelState.IsValid || roleDTO.RoleId != roleId)
            {
                resultDTO.ErrorsMessages = new List<string>();
                resultDTO.ErrorsMessages.Add("Please Add valid Role");
                resultDTO.StatusCode = BadRequest().StatusCode;
                return BadRequest(resultDTO);
            }
            else
            {
                bool isEdited = _roleService.EditRole(roleId, roleDTO);
                if (!isEdited)
                {
                    resultDTO.ErrorsMessages.Add("Role Not Found!!!!!!");
                    resultDTO.StatusCode = NotFound().StatusCode;
                    return NotFound(resultDTO);

                }

                _roleService.SaveChanges();
                resultDTO.StatusCode = Ok().StatusCode;
                resultDTO.Results = roleDTO;
                return Ok(resultDTO);
            }
        }

        /// <summary>
        /// delete role from db
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>

        [HttpDelete("{roleId}")]
        public ActionResult DeleteRole([FromRoute] int roleId)
        {
            ResultDTO<RoleDTO> resultDTO = new ResultDTO<RoleDTO>();
            bool isDeleted = _roleService.DeleteRole(roleId);
            if (!isDeleted)
            {
                resultDTO.ErrorsMessages.Add("Role Not Found!!!!!!");
                resultDTO.StatusCode = NotFound().StatusCode;
                return NotFound(resultDTO);

            }
            _roleService.SaveChanges();
            resultDTO.StatusCode = Ok().StatusCode;
            resultDTO.Messages.Add("Role Deleted Successfully");
            return Ok(resultDTO);

        }

    }
}
