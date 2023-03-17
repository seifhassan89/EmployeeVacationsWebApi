namespace employee_task.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime BirhthDate { get; set; }
        public int GenderId { get; set; }
        public int RoleId { get; set; }
        public Gender Gender { get; set; }

        public Role Role { get; set; }
        public List<VacationRequest> VacationRequests { get; set; }
        public List<VacationBalance> VacationBalances { get; set; }

        public User()
        {
            VacationRequests = new List<VacationRequest>();
            VacationBalances = new List<VacationBalance>();
        }
    }
}