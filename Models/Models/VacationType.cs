using System.ComponentModel.DataAnnotations.Schema;

namespace employee_task.Models
{
    [Table("vacation_type")]

    public class VacationType
    {
        public int VacationTypeId { get; set; }
        public string Name { get; set; }
        public int IntialValue { get; set; }
        public List<VacationBalance> VacationBalances { get; set; }
        public List<VacationRequest> VacationRequests { get; set; }

        public VacationType()
        {
            VacationRequests = new List<VacationRequest>();
            VacationBalances = new List<VacationBalance>();
        }
    }
}