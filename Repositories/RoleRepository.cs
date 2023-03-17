using employee_task.Data;
using employee_task.Models;
using employee_task.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace employee_task.Repositories
{
    public class RoleRepository : BaseRepository, IRoleRepository
    {
        private EmployeeDB _EmployeeDB;
        public RoleRepository(EmployeeDB employeeDB) : base(employeeDB)
        {
            _EmployeeDB = employeeDB;
        }

        /// <summary>
        /// get all roles 
        /// </summary>
        /// <returns></returns>
        public List<Role> GetAll()
        {
            List<Role> roles = (from role in _EmployeeDB.Roles select role).ToList();
            return roles;
        }

        /// <summary>
        /// get role by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Role GetById(int id)
        {
            Role role = new Role();
            role = (from g in _EmployeeDB.Roles where g.RoleId == id select g).FirstOrDefault();
            return role;
        }

        /// <summary>
        /// delete role
        /// </summary>
        /// <param name="role"></param>
        public void DeleteRole(Role role)
        {
            _EmployeeDB.Remove(role);
        }

        /// <summary>
        /// add role to database
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public Role AddRole(Role role)
        {
            EntityEntry<Role> x = _EmployeeDB.Roles.Add(role);
            return x.Entity;

        }

        /// <summary>
        /// edit role data
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="role"></param>
        public bool EditRole(int roleId, Role role)
        {
            Role? existingEntity = _EmployeeDB.Roles.Find(roleId);
            if (existingEntity == null)
            {
                return false;
            }
            else
            {
                _EmployeeDB.Entry(existingEntity).State = EntityState.Detached;
            }
            _EmployeeDB.Attach(role);
            _EmployeeDB.Entry(role).State = EntityState.Modified;
            return true;

        }
    }
}
