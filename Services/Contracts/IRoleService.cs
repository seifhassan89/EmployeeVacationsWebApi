using employee_task.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace employee_task.Services.Contracts
{
    public interface IRoleService
    {
        public List<RoleDTO> GetAll();
        public RoleDTO? GetById(int id);
        public bool EditRole(int roleId, RoleDTO roleDTO);
        public bool DeleteRole(int id);
        public Role AddRole(RoleDTO role);
        public void SaveChanges();

    }
}
