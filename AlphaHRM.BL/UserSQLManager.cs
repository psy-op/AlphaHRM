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
        readonly ILogger<UserSQLManager> logger;
        readonly Mapper mapper;
        readonly Hasher hasher;
        readonly private EFContext dbcontext;
        public UserSQLManager(EFContext dbcontext, Mapper mapper, ILogger<UserSQLManager> logger, Hasher hasher)
        {
            this.dbcontext = dbcontext;
            this.mapper = mapper;
            this.logger = logger;
            this.hasher = hasher;
        }
        public Response<UserDTO> Create(UserDTO user)
        {
            try
            {
                user.ID = new Guid();
                user.Password= hasher.Hash(user.Password);
                dbcontext.User.Add(mapper.Map(user));
                dbcontext.SaveChanges();
                return new Response<UserDTO>(user);
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
                var userentity = dbcontext.User.FirstOrDefault(user => user.ID == id);
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
                var userentity = dbcontext.User.FirstOrDefault(xuser => xuser.ID == user.ID);
                if (userentity == null) { return new Response<UserDTO>(Enums.ErrorCodes.UserNotFound, "User not found."); }
                else
                {
                    //userentity = mapper.Map(user);
                    userentity.Name = user.Name;
                    userentity.Job = user.Job;
                    userentity.ManagerId = user.ManagerId;
                    userentity.Email = user.Email;
                    userentity.Phone = user.Phone;
                    userentity.Type = user.Type;
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
        public async Task<Response<List<UserDTO>>> GetAllUsers()
        {
            try
            {
                List<UserDTO> userlist = new List<UserDTO>();
                var users = dbcontext.User.ToList();
                foreach (var user in users)
                {
                    userlist.Add(mapper.Map(user));
                }

                return new Response<List<UserDTO>>(userlist);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error at GetAllUsers/UserSQLManager");
                return new Response<List<UserDTO>>(Enums.ErrorCodes.Unexpected, "Unexpected error.");
            }
        }


        public async Task<Response<UserDTO>> Login(Guid id, string pass)
        {
            try
            {
                var userentity = dbcontext.User.FirstOrDefault(user => user.ID == id);
                if (userentity == null) { return new Response<UserDTO>(Enums.ErrorCodes.UserNotFound, "User not found."); }
                else
                {
                    if (userentity.Password == hasher.Hash(pass) && userentity.Type)
                    {
                        return new Response<UserDTO>(mapper.Map(userentity));//admin
                    }
                    else if(userentity.Password == hasher.Hash(pass) && !userentity.Type)
                    {
                        return new Response<UserDTO>(mapper.Map(userentity));//user
                    }
                }
                return new Response<UserDTO>(Enums.ErrorCodes.Unexpected, "Unexpected error.");
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex.ToString());
                return new Response<UserDTO>(Enums.ErrorCodes.Unexpected, "Unexpected error.");
            }

        }

        public async Task<Response<List<UserDTO>>> Paging(int size, int page)
        {
            try
            {
                List<UserDTO> userlist = new List<UserDTO>();
                List<UserDTO> items = new List<UserDTO>();
                var users = dbcontext.User.ToList();
                foreach (var user in users)
                {
                    userlist.Add(mapper.Map(user));
                }
                var paging = new Paging(users.Count(), page, size);

                items = userlist.Skip((page) * size).ToList();

                return new Response<List<UserDTO>>(items);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error at GetAllUsers/UserSQLManager");
                return new Response<List<UserDTO>>(Enums.ErrorCodes.Unexpected, "Unexpected error.");
            }
}




    }
}
