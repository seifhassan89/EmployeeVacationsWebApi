using employee_task.Data;
using employee_task.Models;

namespace employee_task.Repositories.Contracts
{
    public interface IGenderRepository : IBaseRepository
    {
        public List<Gender> GetAll();
        public Gender? GetById(int id);

        public Gender AddGender(Gender gender);
        bool EditGender(int genderId, Gender gender);
        public void DeleteGender(Gender gender);
    }
}
