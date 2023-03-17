using employee_task.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace employee_task.Data
{
    public interface IBaseRepository
    {
        public void SaveChanges();
    }
}
