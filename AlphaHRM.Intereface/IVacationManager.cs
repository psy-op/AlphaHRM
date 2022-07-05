using AlphaHRM.Models;

namespace AlphaHRM.Intereface
{
    public interface IVacationManager
    {
        public Task<Response<VacationDTO>> Create(VacationDTO user);
        public Task<Response<VacationDTO>> GetVacation(Guid id);
        public Task<Response<VacationDTO>> Update(VacationDTO vacation);
        public Task<Response<VacationDTO>> Delete(Guid id);
        public Task<PagedResponse<VacationDTO>> GetAllVacations(GetVacationRequest page);
    }
}
