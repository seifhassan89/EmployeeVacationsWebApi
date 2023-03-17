using System.ComponentModel.DataAnnotations.Schema;

namespace employee_task.Models
{
    [Table("vacation_balance")]

    public class VacationBalance
    {
        public int VacationBalanceId { get; set; }
        public int VacationTypeId { get; set; }
        public VacationType VacationType { get; set; }
        public int Balance { get; set; }
        public int Used { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

    }
}