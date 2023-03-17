using System.ComponentModel.DataAnnotations.Schema;

namespace employee_task.Models
{
    public class VacationRequestDTO
    {
        public int VacationRequestId { get; set; }
        public string? UserFullName { get; set; }
        public int UserId { get; set; }
        public bool? Status { get; set; }
        public int VacationTypeId { get; set; }
        public string? VacationType { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}