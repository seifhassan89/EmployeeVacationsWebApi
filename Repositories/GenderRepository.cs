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
    public class GenderRepository : BaseRepository, IGenderRepository
    {
        private EmployeeDB _EmployeeDB;
        public GenderRepository(EmployeeDB employeeDB) : base(employeeDB)
        {
            _EmployeeDB = employeeDB;
        }
        /// <summary>
        /// get all genders
        /// </summary>
        /// <returns></returns>
        public List<Gender> GetAll()
        {
            return _EmployeeDB.Genders.ToList();
        }

        /// <summary>
        /// get gender by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Gender? GetById(int id)
        {
            return _EmployeeDB.Genders.FirstOrDefault(g => g.GenderId == id);
        }
        /// <summary>
        /// delete gender
        /// </summary>
        /// <param name="gender"></param>
        public void DeleteGender(Gender gender)
        {
            _EmployeeDB.Remove(gender);
        }

        /// <summary>
        /// edit gender data
        /// </summary>
        /// <param name="genderId"></param>
        /// <param name="gender"></param>
        public bool EditGender(int genderId, Gender gender)
        {
            Gender? existingEntity = _EmployeeDB.Genders.Find(genderId);
            if (existingEntity == null)
            {
                return false;
            }
            else
            {
                _EmployeeDB.Entry(existingEntity).State = EntityState.Detached;
            }
            _EmployeeDB.Attach(gender);
            _EmployeeDB.Entry(gender).State = EntityState.Modified;
            return true;

        }

        /// <summary>
        /// add gender to database
        /// </summary>
        /// <param name="gender"></param>
        /// <returns></returns>
        public Gender AddGender(Gender gender)
        {
            EntityEntry<Gender> x = _EmployeeDB.Genders.Add(gender);
            return x.Entity;

        }
    }
}
