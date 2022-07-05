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
    public class UserSQLManager : IUserManager
    {
        private readonly ILogger<UserSQLManager> logger;
        private readonly Mapper mapper;
        private readonly Hasher hasher;
        private readonly EFContext dbcontext;
        public UserSQLManager(EFContext dbcontext, Mapper mapper, ILogger<UserSQLManager> logger, Hasher hasher)
        {
            this.dbcontext = dbcontext;
            this.mapper = mapper;
            this.logger = logger;
            this.hasher = hasher;
        }
        public async Task<Response<UserDTO>> Create(UserDTO user)
        {
            try
            {
                var userentity =  dbcontext.User.FirstOrDefault(xuser => xuser.Email == user.Email);
                if (userentity == null)
                {
                    user.ID = new Guid();
                    user.Password = hasher.Hash(user.Password);
                    dbcontext.User.Add(mapper.Map(user));
                    await dbcontext.SaveChangesAsync();
                    return new Response<UserDTO>(user);
                }
                else
                {
                    return new Response<UserDTO>(Enums.ErrorCodes.ExistingUser, "Existing User.");
                }

            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error at Create/UserSQLManager");
                return new Response<UserDTO>(Enums.ErrorCodes.Unexpected, "Unexpected error.");
            }
        }
        public async Task<Response<UserDTO>> GetUser(Guid id)
        {
            try
            {
                var userentity =  dbcontext.User.FirstOrDefault(user => user.ID == id);
                if (userentity == null) { return new Response<UserDTO>(Enums.ErrorCodes.UserNotFound, "User not found."); }
                else { return new Response<UserDTO>(mapper.Map(userentity)); }
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error at GetUser/UserSQLManager");
                return new Response<UserDTO>(Enums.ErrorCodes.Unexpected, "Unexpected error.");
            }
        }
        public async Task<Response<UserDTO>> Update(UserDTO user)
        {
            try
            {
                var userentity = dbcontext.User.FirstOrDefault(xuser => xuser.Email == user.Email);
                if (userentity == null) { return new Response<UserDTO>(Enums.ErrorCodes.UserNotFound, "User not found."); }
                else
                {
                    //userentity = mapper.Map(user);
                    userentity.Name = user.Name;
                    userentity.Job = user.Job;
                    userentity.ManagerId = user.ManagerId;
                    userentity.Email = user.Email;
                    userentity.Phone = user.Phone;
                    userentity.Type = (int)user.Type;
                    userentity.Password = hasher.Hash(user.Password);
                    await dbcontext.SaveChangesAsync();
                }
                return new Response<UserDTO>(user);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error at Update/UserSQLManager");
                return new Response<UserDTO>(Enums.ErrorCodes.Unexpected, "Unexpected error.");
            }
        }
        public async Task<Response<UserDTO>> Delete(Guid id)
        {
            try
            {
                var userentity = dbcontext.User.FirstOrDefault(user => user.ID == id);
                if (userentity == null) { return new Response<UserDTO>(Enums.ErrorCodes.UserNotFound, "User not found."); }
                else { dbcontext.User.Remove(userentity); }
                await dbcontext.SaveChangesAsync();
                return new Response<UserDTO>(Enums.ErrorCodes.Success, "Success");
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error at Delete/UserSQLManager");
                return new Response<UserDTO>(Enums.ErrorCodes.Unexpected, "Unexpected error.");
            }

        }
        public async Task<PagedResponse<UserDTO>> GetAllUsers(GetUsersRequest page)
        {
            try
            {
                var users = dbcontext.User
                    .Skip((page.PageNumber - 1) * page.PageSize)
                    .Take(page.PageSize)
                    .ToList();


                List<UserDTO> userlist = new();
                foreach (var user in users)
                {
                    if (user.ManagerId == null){ userlist.Add(mapper.Map(user)); }          
                }

                var totalCount = await dbcontext.User.CountAsync();
                return new PagedResponse<UserDTO> (userlist, totalCount);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error at GetAllUsers/UserSQLManager");
                return new PagedResponse<UserDTO>(Enums.ErrorCodes.Unexpected, "Unexpected error.");
            }
        }


        public async Task<Response<UserDTO>> Login(LoginRequest req)
        {
            try
            {
                var userentity = await dbcontext.User.FirstOrDefaultAsync(user => user.Email == req.Email);
                if (userentity == null) { return new Response<UserDTO>(Enums.ErrorCodes.UserNotFound, "User not found."); }
                else
                {
                    if (userentity.Password == hasher.Hash(req.Password))
                    {
                        return new Response<UserDTO>(mapper.Map(userentity));
                    }
                   
                return new Response<UserDTO>(Enums.ErrorCodes.InvalidLogin, "Invalid Login");
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error at Login/UserSQLManager");
                return new Response<UserDTO>(Enums.ErrorCodes.Unexpected, "Unexpected error.");
            }

        }




    }
}
