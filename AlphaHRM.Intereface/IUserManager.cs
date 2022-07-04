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
        public Response<UserDTO> Create(UserDTO user);
        public Task<Response<UserDTO>> GetUser(Guid id);
        public Task<Response<UserDTO>> Update(UserDTO user);
        public Task<Response<UserDTO>> Delete(Guid id);
        public Task<Response<List<UserDTO>>> GetAllUsers();
        public Task<Response<UserDTO>> Login(Guid id, string pass);
        public Task<Response<List<UserDTO>>> Paging(int size, int page);

    }
}
