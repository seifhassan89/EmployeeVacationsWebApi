namespace employee_task.Models
{
    public class Gender
    {
        public int GenderId { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
        public Gender()
        {
            Users = new List<User>();
        }
    }
}