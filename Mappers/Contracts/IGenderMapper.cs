using employee_task.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace employee_task.Mappers.Contracts
{
    public interface IGenderMapper
    {
        public Gender MapToGender(GenderDTO genderDTO);
        public GenderDTO MapToGenderDTO(Gender gender);
    }
}
