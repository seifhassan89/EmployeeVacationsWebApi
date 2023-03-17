namespace employee_task.Models
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime BirhthDate { get; set; }
        public int GenderId { get; set; }
        public string? Gender { get; set; }
        public int RoleId { get; set; }
        public string? Role { get; set; }

        public List<VacationBalanceDTO> vacationBalanceDTOs { get; set; }

        public UserDTO()
        {
            vacationBalanceDTOs = new List<VacationBalanceDTO>();

        }
    }
}