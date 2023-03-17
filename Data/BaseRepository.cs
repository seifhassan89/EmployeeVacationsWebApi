using employee_task.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace employee_task.Data
{
    public abstract class BaseRepository: IBaseRepository
    {
        private readonly EmployeeDB _employeeDb;
        public BaseRepository(EmployeeDB employeeDB)
        {
            _employeeDb = employeeDB;
        }

        public void SaveChanges()
        {
            _employeeDb.SaveChanges();
        }
    }
}
