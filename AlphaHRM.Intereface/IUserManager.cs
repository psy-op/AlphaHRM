using AlphaHRM.Models;

namespace AlphaHRM.Intereface
{
    public interface IUserManager
    {
        public Task<Response<UserDTO>> Create(UserDTO user);
        public Task<Response<UserDTO>> GetUser(Guid id);
        public Task<Response<UserDTO>> Update(UserDTO user);
        public Task<Response<UserDTO>> Delete(Guid id);
        public Task<PagedResponse<UserDTO>> GetAllUsers(GetUsersRequest page);
        public Task<Response<string>> Login(LoginRequest req);

    }
}
