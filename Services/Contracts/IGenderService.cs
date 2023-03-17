using employee_task.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace employee_task.Services.Contracts
{
    public interface IGenderService
    {
        public List<GenderDTO> GetAll();
        public GenderDTO? GetById(int id);
        public bool EditGender(int genderId, GenderDTO genderDTO);
        public bool DeleteGender(int id);
        public Gender AddGender(GenderDTO gender);
        public void SaveChanges();


    }
}
