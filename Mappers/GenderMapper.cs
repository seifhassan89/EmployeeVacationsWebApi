using employee_task.Mappers.Contracts;
using employee_task.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace employee_task.Mappers
{
    public class GenderMapper : IGenderMapper
    {

        public GenderDTO MapToGenderDTO(Gender gender)
        {

            GenderDTO genderDTO = new GenderDTO();
            genderDTO.GenderId = gender.GenderId;
            genderDTO.Name = gender.Name;
            return genderDTO;
        }

        public Gender MapToGender(GenderDTO genderDTO)
        {
            Gender gender = new Gender();
            gender.GenderId = genderDTO.GenderId;
            gender.Name = genderDTO.Name;
            return gender;
        }
    }
}
