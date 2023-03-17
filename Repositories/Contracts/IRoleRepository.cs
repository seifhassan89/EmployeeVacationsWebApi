using employee_task.Data;
using employee_task.Models;

namespace employee_task.Repositories.Contracts
{
    public interface IRoleRepository : IBaseRepository
    {
        public List<Role> GetAll();
        public Role? GetById(int id);

        public Role AddRole(Role role);
        bool EditRole(int roleId, Role role);
        public void DeleteRole(Role role);
    }
}
