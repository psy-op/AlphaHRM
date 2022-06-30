using AlphaHRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaHRM.Intereface
{
    public interface IVacationManager
    {
        public VacationDTO Create(VacationDTO user);
        public VacationDTO Retrive(Guid id);
        public VacationDTO Update(VacationDTO vacation);
        public void Delete(Guid id);
        public List<VacationDTO> GetAll();
    }
}
