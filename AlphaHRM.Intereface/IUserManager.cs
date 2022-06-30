using AlphaHRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaHRM.Intereface
{
    public interface IUserManager
    {
        public UserDTO Create(UserDTO user);
        public UserDTO Retrive(Guid id);
        public UserDTO Update(UserDTO user);
        public void Delete(Guid id);
        public List<UserDTO> GetAll();

    }
}
