using employee_task.Mappers.Contracts;
using employee_task.Models;
using employee_task.Repositories.Contracts;
using employee_task.Services.Contracts;

namespace employee_task.Services
{
    public class VacationBalanceService : IVacationBalanceService
    {
        private readonly IVacationBalanceRepository _vacationBalanceRepository;
        private readonly IVacationBalanceMapper _vacationBalanceMapper;
        public VacationBalanceService(IVacationBalanceRepository vacationBalanceRepository, IVacationBalanceMapper vacationBalanceMapper)
        {
            _vacationBalanceRepository = vacationBalanceRepository;
            _vacationBalanceMapper = vacationBalanceMapper;
        }

        /// <summary>
        /// get all vacation Balances DTO
        /// </summary>
        /// <returns></returns>
        public List<VacationBalanceDTO> GetAllByUserID(int userID)
        {
            List<VacationBalanceDTO> vacationBalances = new List<VacationBalanceDTO>();

            List<VacationBalance> vacationBalanceEntities = _vacationBalanceRepository.GetAllByUserID(userID);
            vacationBalanceEntities.ForEach(vacationBalanceEntity =>
            {
                //call mapper 
                VacationBalanceDTO vacationBalanceDTO = _vacationBalanceMapper.MapToVacationBalanceDTO(vacationBalanceEntity);
                vacationBalances.Add(vacationBalanceDTO);
            });

            return vacationBalances;
        }

        /// <summary>
        /// get vacation Balance DTO by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VacationBalanceDTO? GetById(int id)
        {
            VacationBalance? vacationBalance = _vacationBalanceRepository.GetById(id);
            if (vacationBalance == null)
            {
                return null;
            }
            return _vacationBalanceMapper.MapToVacationBalanceDTO(vacationBalance);

        }

        /// <summary>
        /// add user to DB
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public VacationBalance AddVacationBalance(VacationBalanceDTO vacationBalance)
        {
            VacationBalance vacationBalanceEntity = _vacationBalanceMapper.MapToVacationBalance(vacationBalance);
            VacationBalance AddVacationBalance = _vacationBalanceRepository.AddVacationBalance(vacationBalanceEntity);
            return AddVacationBalance;
        }

        /// <summary>
        /// edit vacation balance Data 
        /// </summary>
        /// <param name="vacationBalanceId"></param>
        /// <param name="vacationBalanceBody"></param>
        /// <returns></returns>
        public bool EditVacationBalaceByVacationBalanceId(int vacationBalanceId, VacationBalanceDTO vacationBalanceBody)
        {
            VacationBalance vacationBalanceEntity = _vacationBalanceMapper.MapToVacationBalance(vacationBalanceBody);
            return _vacationBalanceRepository.EditVacationBalance(vacationBalanceId, vacationBalanceEntity);
        }

        public void SaveChanges()
        {
            _vacationBalanceRepository.SaveChanges();
        }

        /// <summary>
        /// update vacation balance Data by accepting of reject vacation request
        /// </summary>
        /// <param name="vacationRequest"></param>
        /// <returns></returns>
        public bool UpdateBalance(VacationRequest vacationRequest)
        {
            VacationBalance? vacationBalance = _vacationBalanceRepository.GetVacationBalanceByUserIdAndVacationType(vacationRequest.UserId, vacationRequest.VacationTypeId);
            if (vacationBalance == null)
            {
                return false;
            }

            int durationInDays = vacationRequest.EndDate.Subtract(vacationRequest.StartDate).Days;
            vacationBalance.Balance -= durationInDays;
            vacationBalance.Used += durationInDays;
            _vacationBalanceRepository.EditVacationBalance(vacationBalance.VacationBalanceId, vacationBalance);
            return true;

        }

        /// <summary>
        /// delete vacationBalance by id
        /// </summary>
        /// <param name="vacationBalanceId"></param>
        /// <returns></returns>
        public bool DeleteVacationBalance(int vacationBalanceId)
        {
            VacationBalance? vacationBalance = _vacationBalanceRepository.GetById(vacationBalanceId); ;
            if (vacationBalance == null)
            {
                return false;
            }
            _vacationBalanceRepository.DeleteVacationBalance(vacationBalance);
            return true;
        }


    }
}
