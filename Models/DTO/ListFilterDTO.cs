namespace employee_task.Models
{
    public class ListFilterDTO
    {
        public bool? Status { get; set; }
        public int UserId { get; set; }
        public int VacationTypeId { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        public ListFilterDTO()
        {
        }
    }
}