using AlphaHRM.DAL;
using AlphaHRM.Intereface;
using AlphaHRM.Models;
using AlphaHRM.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaHRM.BL
{
    public class VacationSQLManager : IVacationManager
    {
        private readonly ILogger<VacationSQLManager> logger;
        private readonly Mapper mapper;
        private readonly EFContext dbcontext;
        public VacationSQLManager(EFContext dbcontext, Mapper mapper, ILogger<VacationSQLManager> logger)
        {
            this.dbcontext = dbcontext;
            this.mapper = mapper;
            this.logger = logger;
        }
        public async Task<Response<VacationDTO>> Create(VacationDTO vacation)
        {
            try
            {
                dbcontext.Vacation.Add(mapper.Map(vacation));
                await dbcontext.SaveChangesAsync();
                return new Response<VacationDTO>(vacation);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error at Create/VacationSQLManager");
                return new Response<VacationDTO>(Enums.ErrorCodes.Unexpected, "Unexpected error.");

            }
        }
        public async Task<Response<VacationDTO>> GetVacation(Guid id)
        {
            try
            {
                var vacationentity = dbcontext.Vacation.FirstOrDefault(vacation => vacation.ID == id);
                if (vacationentity == null) { return new Response<VacationDTO>(Enums.ErrorCodes.VacationNotFound, "Vacation not found."); }
                else { return new Response<VacationDTO>(mapper.Map(vacationentity)); }
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error at GetVacation/VacationSQLManager");
                return new Response<VacationDTO>(Enums.ErrorCodes.Unexpected, "Unexpected error.");

            }
        }
        public async Task<Response<VacationDTO>> Update(VacationDTO vacation)
        {
            try
            {
                var vacationentity =  dbcontext.Vacation.FirstOrDefault(xvacation => xvacation.ID == vacation.ID);
                if (vacationentity == null) { return new Response<VacationDTO>(Enums.ErrorCodes.VacationNotFound, "Vacation not found."); }
                else
                {
                    vacationentity.Type = (int)vacation.Type;
                    vacationentity.Duration = vacation.Duration;
                    vacationentity.Date = vacation.Date;
                    vacationentity.Status = (int)vacation.Status;
                    vacationentity.Note = vacation.Note;
                    vacationentity.IsDraft = (int)vacation.IsDraft;
                    vacationentity.UserID = vacation.UserID;
                    await dbcontext.SaveChangesAsync();
                }
                return new Response<VacationDTO>(vacation);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error at Update/VacationSQLManager");
                return new Response<VacationDTO>(Enums.ErrorCodes.Unexpected, "Unexpected error.");
            }
        }
        public async Task<Response<VacationDTO>> Delete(Guid id)
        {
            try
            {
                var vacationentity = dbcontext.Vacation.FirstOrDefault(vacation => vacation.ID == id);
                if (vacationentity == null) { return new Response<VacationDTO>(Enums.ErrorCodes.VacationNotFound, "Vacation not found."); }
                else { dbcontext.Vacation.Remove(vacationentity); }
                await dbcontext.SaveChangesAsync();
                return new Response<VacationDTO>(Enums.ErrorCodes.Success, "Success");
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error at Delete/VacationSQLManager");
                return new Response<VacationDTO>(Enums.ErrorCodes.Unexpected, "Unexpected error.");
            }
        }
        public async Task<PagedResponse<VacationDTO>> GetAllVacations(GetVacationRequest page)
        {
            try
            {
                var vacys = dbcontext.Vacation
                    .Skip((page.PageNumber - 1) * page.PageSize)
                    .Take(page.PageSize)
                    .ToList();
                List<VacationDTO> vacylist = new();              
                foreach (var vacy in vacys)
                {
                    vacylist.Add(mapper.Map(vacy));
                }
                var totalCount = await dbcontext.Vacation.CountAsync();
                return new PagedResponse<VacationDTO>(vacylist, totalCount);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error at GetAllUsers/UserSQLManager");
                return new PagedResponse<VacationDTO>(Enums.ErrorCodes.Unexpected, "Unexpected error.");
            }
        }
    }
}
