using AlphaHRM.DAL;
using AlphaHRM.Intereface;
using AlphaHRM.Models;
using AlphaHRM.Utilities;
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
        readonly ILogger<VacationSQLManager> logger;
        readonly Mapper mapper;
        readonly private EFContext dbcontext;
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
                var vacationentity = dbcontext.Vacation.FirstOrDefault(xvacation => xvacation.ID == vacation.ID);
                if (vacationentity == null) { return new Response<VacationDTO>(Enums.ErrorCodes.VacationNotFound, "Vacation not found."); }
                else
                {
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
        public async Task<Response<List<VacationDTO>>> GetAllVacations()
        {
            try
            {
                List<VacationDTO> vacationlist = new List<VacationDTO>();
                var vacations = dbcontext.Vacation.ToList();
                foreach (var vacy in vacations)
                {
                    vacationlist.Add(mapper.Map(vacy));
                }

                return new Response<List<VacationDTO>>(vacationlist);
            }
            catch(Exception ex)
            {
                logger.LogCritical(ex, "Error at GetAllVacations/VacationSQLManager");
                return new Response<List<VacationDTO>>(Enums.ErrorCodes.Unexpected, "Unexpected error.");
            }
        }
    }
}
