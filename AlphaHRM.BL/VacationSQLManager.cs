using AlphaHRM.DAL;
using AlphaHRM.Intereface;
using AlphaHRM.Models;
using LMS.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaHRM.BL
{
    public class VacationSQLManager : IVacationManager
    {
        ClassMapper mapper;
        private EFContext dbcontext;
        public VacationSQLManager(EFContext _context, ClassMapper _mapper)
        {
            dbcontext = _context;
            mapper = _mapper;
        }
        public VacationDTO Create(VacationDTO vacation)
        {
            try
            {
                dbcontext.Vacation.Add(mapper.Map(vacation));
                dbcontext.SaveChanges();
                return vacation;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public VacationDTO Retrive(Guid id)
        {
            try
            {
                var vacationentity = dbcontext.Vacation.FirstOrDefault(vacation => vacation.ID == id);
                if (vacationentity == null) { return null; }
                else { return mapper.Map(vacationentity); }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public VacationDTO Update(VacationDTO vacation)
        {
            try
            {
                var vacationentity = dbcontext.Vacation.FirstOrDefault(xvacation => xvacation.ID == vacation.ID);
                if (vacationentity == null) { return null; }
                else
                {
                    dbcontext.SaveChanges();
                }
                return vacation;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void Delete(Guid id)
        {
            try
            {
                var vacationentity = dbcontext.Vacation.FirstOrDefault(vacation => vacation.ID == id);
                if (vacationentity == null) { return; }
                else { dbcontext.Vacation.Remove(vacationentity); }
                dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                return;
            }
        }
        public List<VacationDTO> GetAll()
        {
            List<VacationDTO> vacationlist = new List<VacationDTO>();
            var vacations = dbcontext.Vacation.ToList();
            foreach(var vacy in vacations)
            {
                vacationlist.Add(mapper.Map(vacy));
            }

            return vacationlist;
        }
    }
}
