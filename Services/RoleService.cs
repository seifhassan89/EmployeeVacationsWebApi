using employee_task.Mappers.Contracts;
using employee_task.Models;
using employee_task.Repositories.Contracts;
using employee_task.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace employee_task.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IRoleMapper _roleMapper;
        public RoleService(IRoleRepository roleRepository, IRoleMapper roleMapper)
        {
            _roleRepository = roleRepository;
            _roleMapper = roleMapper;
        }

        /// <summary>
        /// get list of roles DTO
        /// </summary>
        /// <returns></returns>
        public List<RoleDTO> GetAll()
        {
            List<RoleDTO> roles = new List<RoleDTO>();
            roles = _roleRepository.GetAll().Select(x => _roleMapper.MapToRoleDTO(x)).ToList();
            return roles;
        }

        /// <summary>
        /// get role DTO by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RoleDTO? GetById(int id)
        {
            Role? role = _roleRepository.GetById(id);
            if (role == null)
            {
                return null;
            }
            RoleDTO roleDTO = _roleMapper.MapToRoleDTO(role);
            return roleDTO;
        }



        /// <summary>
        /// add role to db
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public Role AddRole(RoleDTO role)
        {
            Role roleEntity = _roleMapper.MapToRole(role);
            return _roleRepository.AddRole(roleEntity);
        }

        /// <summary>
        /// delete role 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteRole(int id)
        {
            Role? role = _roleRepository.GetById(id); ;
            if (role == null)
            {
                return false;
            }
            _roleRepository.DeleteRole(role);
            return true;

        }

        /// <summary>
        /// edit role data 
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="roleDTO"></param>
        /// <returns></returns>
        public bool EditRole(int roleId, RoleDTO roleDTO)
        {
            Role roleEntity = _roleMapper.MapToRole(roleDTO);
            return _roleRepository.EditRole(roleId, roleEntity);
        }

        /// <summary>
        /// save changes to DB
        /// </summary>
        public void SaveChanges()
        {
            _roleRepository.SaveChanges();
        }
    }
}
