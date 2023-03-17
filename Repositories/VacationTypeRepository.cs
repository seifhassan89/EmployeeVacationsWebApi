using employee_task.Data;
using employee_task.Models;
using employee_task.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;

namespace employee_task.Repositories
{
    public class VacationTypeRepository : BaseRepository, IVacationTypeRepository
    {
        private EmployeeDB _EmployeeDB;
        public VacationTypeRepository(EmployeeDB employeeDB) : base(employeeDB)
        {
            _EmployeeDB = employeeDB;
        }

        /// <summary>
        /// add vacation type
        /// </summary>
        /// <param name="vacationType"></param>
        /// <returns></returns>
        public VacationType AddVacationType(VacationType vacationType)
        {
            EntityEntry<VacationType> x = _EmployeeDB.VacationTypes.Add(vacationType);
            return x.Entity;

        }

        /// <summary>
        /// delete vacation Type
        /// </summary>
        /// <param name="vacationType"></param>
        public void DeleteVacationType(VacationType vacationType)
        {
            _EmployeeDB.Remove(vacationType);
        }

        /// <summary>
        /// edit vacation type data
        /// </summary>
        /// <param name="vacationTypeId"></param>
        /// <param name="vacationType"></param>
        /// <returns></returns>
        public bool EditVacationType(int vacationTypeId, VacationType vacationType)
        {
            VacationType? existingEntity = _EmployeeDB.VacationTypes.Find(vacationTypeId);

            if (existingEntity == null)
            {
                return false;
            }
            else
            {
                _EmployeeDB.Entry(existingEntity).State = EntityState.Detached;
            }
            _EmployeeDB.Attach(vacationType);
            _EmployeeDB.Entry(vacationType).State = EntityState.Modified;
            return true;
        }

        /// <summary>
        /// get all vacation types
        /// </summary>
        /// <returns></returns>
        public List<VacationType> GetAll()
        {
            List<VacationType> vacationTypes = new List<VacationType>();
            vacationTypes = _EmployeeDB.VacationTypes.ToList();
            return vacationTypes;
        }

        /// <summary>
        /// get vacation type by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VacationType? GetById(int id)
        {
            VacationType? vacationTypeEntity = _EmployeeDB.VacationTypes.Where(u => u.VacationTypeId == id).FirstOrDefault();
            return vacationTypeEntity;
        }
    }
}
