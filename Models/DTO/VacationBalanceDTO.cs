using System.ComponentModel.DataAnnotations.Schema;

namespace employee_task.Models
{

    public class VacationBalanceDTO
    {
        public int VacationBalanceId { get; set; }
        public int VacationTypeId { get; set; }
        public string VacationType { get; set; }
        public int Balance { get; set; }
        public int Used { get; set; }
        public int UserId { get; set; }

    }
}