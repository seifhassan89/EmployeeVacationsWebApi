using employee_task.Mappers.Contracts;
using employee_task.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace employee_task.Mappers
{
    public class RoleMapper : IRoleMapper
    {

        public RoleDTO MapToRoleDTO(Role role)
        {

            RoleDTO roleDTO = new RoleDTO();
            roleDTO.RoleId = role.RoleId;
            roleDTO.Name = role.Name;
            return roleDTO;
        }

        public Role MapToRole(RoleDTO roleDTO)
        {
            Role role = new Role();
            role.RoleId = roleDTO.RoleId;
            role.Name = roleDTO.Name;
            return role;
        }
    }
}
