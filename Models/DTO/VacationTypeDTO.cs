using System.ComponentModel.DataAnnotations.Schema;

namespace employee_task.Models
{
    public class VacationTypeDTO
    {
        public int VacationTypeId { get; set; }
        public string Name { get; set; }
        public int IntialValue { get; set; }

        public VacationTypeDTO()
        {

        }
    }
}