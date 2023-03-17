using employee_task.Data;
using employee_task.Models;
using employee_task.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace employee_task.Repositories
{
    public class VacationRequestRepository : BaseRepository, IVacationRequestRepository
    {
        private EmployeeDB _EmployeeDB;
        public VacationRequestRepository(EmployeeDB employeeDB) : base(employeeDB)
        {
            _EmployeeDB = employeeDB;
        }

        /// <summary>
        /// add request vacation to DB
        /// </summary>
        /// <param name="vacationRequest"></param>
        /// <returns></returns>
        public VacationRequest AddVacationRequest(VacationRequest vacationRequest)
        {
            EntityEntry<VacationRequest> x = _EmployeeDB.VacationRequests.Add(vacationRequest);
            return x.Entity;

        }

        /// <summary>
        /// delete Request vacation
        /// </summary>
        /// <param name="vacationRequest"></param>
        public void DeleteVacationRequest(VacationRequest vacationRequest)
        {
            _EmployeeDB.Remove(vacationRequest);
        }

        /// <summary>
        /// edit request data
        /// </summary>
        /// <param name="vacationRequestId"></param>
        /// <param name="vacationRequest"></param>
        /// <returns></returns>
        public bool EditVacationRequest(int vacationRequestId, VacationRequest vacationRequest)
        {
            VacationRequest? existingEntity = _EmployeeDB.VacationRequests.Find(vacationRequestId);
            if (existingEntity == null)
            {
                return false;
            }
            else
            {
                _EmployeeDB.Entry(existingEntity).State = EntityState.Detached;
            }
            _EmployeeDB.Attach(vacationRequest);
            _EmployeeDB.Entry(vacationRequest).State = EntityState.Modified;
            return true;
        }

        /// <summary>
        /// get request Details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VacationRequestDTO GetById(int id)
        {
            VacationRequestDTO? vacationRequestDto = (from vacationRequest in _EmployeeDB.VacationRequests

                                                      join user in _EmployeeDB.Users
                                                      on vacationRequest.UserId equals user.UserId

                                                      join vacationType in _EmployeeDB.VacationTypes
                                                      on vacationRequest.VacationTypeId equals vacationType.VacationTypeId

                                                      where vacationRequest.VacationRequestId == id
                                                      select new VacationRequestDTO
                                                      {
                                                          VacationRequestId = vacationRequest.VacationRequestId,
                                                          VacationTypeId = vacationRequest.VacationTypeId,
                                                          UserFullName = user != null ? user.FullName : String.Empty,
                                                          UserId = user.UserId,
                                                          Status = vacationRequest.Status,
                                                          EndDate = vacationRequest.EndDate,
                                                          StartDate = vacationRequest.StartDate,
                                                          VacationType = vacationType != null ? vacationType.Name : String.Empty
                                                      }
                                                     ).FirstOrDefault();
            return vacationRequestDto;
        }
        /// <summary>
        /// get request entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VacationRequest? GetEntityById(int id)
        {
            VacationRequest? vacationRequest = _EmployeeDB.VacationRequests.FirstOrDefault(x => x.VacationRequestId == id);
            return vacationRequest;
        }

        /// <summary>
        /// get list of vacation requests for a specific user 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<VacationRequestDTO> GetRequestsByUserId(int id)
        {
            List<VacationRequestDTO>? vacationRequests = (from vacationRequest in _EmployeeDB.VacationRequests
                                                          join user in _EmployeeDB.Users
                                                          on vacationRequest.UserId equals user.UserId

                                                          join VacationType in _EmployeeDB.VacationTypes
                                                          on vacationRequest.VacationTypeId equals VacationType.VacationTypeId

                                                          where vacationRequest.UserId == id
                                                          select new VacationRequestDTO
                                                          {
                                                              VacationRequestId = vacationRequest.VacationRequestId,
                                                              VacationTypeId = vacationRequest.VacationTypeId,
                                                              UserFullName = user != null ? user.FullName : String.Empty,
                                                              UserId = user.UserId,
                                                              Status = vacationRequest.Status,
                                                              EndDate = vacationRequest.EndDate,
                                                              StartDate = vacationRequest.StartDate,
                                                              VacationType = VacationType != null ? VacationType.Name : String.Empty
                                                          }
                                                     ).ToList();
            return vacationRequests;
        }

        /// <summary>
        /// get request list filtered and paginated
        /// </summary>
        /// <param name="listFilterDTO"></param>
        /// <returns></returns>
        public List<VacationRequestDTO> VacationRequestList(ListFilterDTO listFilterDTO)
        {
            int skiip = (listFilterDTO.CurrentPage - 1) * listFilterDTO.PageSize;

            List<VacationRequestDTO> vacationRequestList = (from vacationRequest in _EmployeeDB.VacationRequests

                                                            join user in _EmployeeDB.Users
                                                            on vacationRequest.UserId equals user.UserId

                                                            join VacationType in _EmployeeDB.VacationTypes
                                                            on vacationRequest.VacationTypeId equals VacationType.VacationTypeId

                                                            where
                                                            // user id
                                                            (listFilterDTO.UserId == 0 || listFilterDTO.UserId == vacationRequest.UserId) &&
                                                           //vacation type
                                                           (listFilterDTO.VacationTypeId == 0 || listFilterDTO.VacationTypeId == vacationRequest.VacationTypeId) &&
                                                           //status
                                                           (listFilterDTO.Status == vacationRequest.Status)
                                                            select new VacationRequestDTO
                                                            {
                                                                Status = vacationRequest.Status,
                                                                EndDate = vacationRequest.EndDate,
                                                                StartDate = vacationRequest.StartDate,
                                                                UserFullName = user.FullName,
                                                                UserId = user.UserId,
                                                                VacationRequestId = vacationRequest.VacationRequestId,
                                                                VacationType = VacationType.Name,
                                                                VacationTypeId = vacationRequest.VacationTypeId
                                                            }).Skip(skiip).Take(listFilterDTO.PageSize).ToList();
            return vacationRequestList;
        }

        /// <summary>
        /// get total number of requests for a specific filters
        /// </summary>
        /// <param name="listFilterDTO"></param>
        /// <returns></returns>
        public int CountVacationRequest(ListFilterDTO listFilterDTO)
        {
            int count = (from vacationRequest in _EmployeeDB.VacationRequests

                         where
                         // user id
                         (listFilterDTO.UserId == 0 || listFilterDTO.UserId == vacationRequest.UserId) &&
                        //type
                        (listFilterDTO.VacationTypeId == 0 || listFilterDTO.VacationTypeId == vacationRequest.VacationTypeId) &&
                        //status
                        (listFilterDTO.Status == vacationRequest.Status)
                         select vacationRequest).Count();
            return count;
        }
    }
}
