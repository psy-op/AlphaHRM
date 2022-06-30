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
    public class UserSQLManager : IUserManager
    {
        ClassMapper mapper;
        private EFContext dbcontext;
        public UserSQLManager(EFContext _context, ClassMapper _mapper)
        {
            dbcontext = _context;
            mapper = _mapper;
        }
        public UserDTO Create(UserDTO user)
        {
            try
            { 
                dbcontext.User.Add(mapper.Map(user));
                dbcontext.SaveChanges();
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public UserDTO Retrive(Guid id)
        {
            try
            {
                var userentity = dbcontext.User.FirstOrDefault(user => user.ID == id);
                if (userentity == null) { return null; }
                else { return mapper.Map(userentity); }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public UserDTO Update(UserDTO user)
        {
            try
            {
                var userentity = dbcontext.User.FirstOrDefault(xuser => xuser.ID == user.ID);
                if (userentity == null) { return null; }
                else
                {
                    dbcontext.SaveChanges();
                }
                return user;
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
                var userentity = dbcontext.User.FirstOrDefault(user => user.ID == id);
                if (userentity == null) { return; }
                else { dbcontext.User.Remove(userentity); }
                dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                return;
            }

        }
        public List<UserDTO> GetAll()
        {

            List<UserDTO> userlist = new List<UserDTO>();
            var users = dbcontext.User.ToList();
            foreach (var user in users)
            {
                userlist.Add(mapper.Map(user));
            }

            return userlist;
        }
    }
}
