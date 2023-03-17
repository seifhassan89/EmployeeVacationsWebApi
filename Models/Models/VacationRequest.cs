using System.ComponentModel.DataAnnotations.Schema;

namespace employee_task.Models
{
    [Table("vacation_request")]

    public class VacationRequest
    {
        public int VacationRequestId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public bool? Status { get; set; }
        public int VacationTypeId { get; set; }
        public VacationType VacationType { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}