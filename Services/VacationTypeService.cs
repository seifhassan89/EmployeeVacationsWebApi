using employee_task.Data;
using employee_task.Mappers;
using employee_task.Mappers.Contracts;
using employee_task.Models;
using employee_task.Repositories.Contracts;
using employee_task.Services.Contracts;
using System.Collections.Generic;

namespace employee_task.Services
{
    public class VacationTypeService : IVacationTypeService
    {
        private readonly IVacationTypeRepository _vacationTypeRepository;
        private readonly IVacationTypeMapper _vacationTypeMapper;
        public VacationTypeService(IVacationTypeRepository vacationTypeRepository, IVacationTypeMapper vacationTypeMapper)
        {
            _vacationTypeRepository = vacationTypeRepository;
            _vacationTypeMapper = vacationTypeMapper;
        }
        /// <summary>
        /// add vacation type
        /// </summary>
        /// <param name="vacationType"></param>
        /// <returns></returns>
        public VacationType AddVacationType(VacationTypeDTO vacationType)
        {
            VacationType vacationTypeEntity = _vacationTypeMapper.MapToVacationType(vacationType);
            VacationType AddVacationType = _vacationTypeRepository.AddVacationType(vacationTypeEntity);

            return AddVacationType;
        }

        /// <summary>
        /// delete vacation type by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteVacationType(int id)
        {
            VacationType? vacationType = _vacationTypeRepository.GetById(id);
            if (vacationType == null)
            {
                return false;
            }
            _vacationTypeRepository.DeleteVacationType(vacationType);
            return true;
        }

        /// <summary>
        /// edit vacation type data
        /// </summary>
        /// <param name="vacationTypeId"></param>
        /// <param name="vacationTypeDTO"></param>
        /// <returns></returns>
        public bool EditVacationType(int vacationTypeId, VacationTypeDTO vacationTypeDTO)
        {
            VacationType vacationTypeEntity = _vacationTypeMapper.MapToVacationType(vacationTypeDTO);
            return _vacationTypeRepository.EditVacationType(vacationTypeId, vacationTypeEntity);
        }

        /// <summary>
        /// get All vacation types
        /// </summary>
        /// <returns></returns>
        public List<VacationTypeDTO> GetAll()
        {
            List<VacationTypeDTO> vacationTypes = new List<VacationTypeDTO>();
            vacationTypes = _vacationTypeRepository.GetAll().Select(x => _vacationTypeMapper.MapToVacationTypeDTO(x)).ToList();

            return vacationTypes;
        }

        /// <summary>
        /// get vacation type by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VacationTypeDTO? GetById(int id)
        {
            VacationType? vacationType = _vacationTypeRepository.GetById(id);
            if (vacationType != null)
            {
                VacationTypeDTO vacationTypeDTO = _vacationTypeMapper.MapToVacationTypeDTO(vacationType);
                return vacationTypeDTO;
            }
            else
            {
                return null;

            }

        }
        public void SaveChanges()
        {
            _vacationTypeRepository.SaveChanges();
        }
    }
}
