using AlphaHRM.DAL.EF;
using AlphaHRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaHRM.Utilities
{
    public class Mapper
    {
        public UserDTO Map(UserEntity user)
        {
            var temp = new UserDTO
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Job = user.Job,
                Phone = user.Phone,
                Type = user.Type,
                ID = user.ID,
                ManagerId = user.ManagerId

            };
            return temp;
        }

        public UserEntity Map(UserDTO user)
        {
            var temp = new UserEntity
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Job = user.Job,
                Phone = user.Phone,
                Type = user.Type,
                ID = user.ID,
                ManagerId = user.ManagerId,
                Created = DateTime.Now

        };
            return temp;
        }


        public VacationDTO Map(VacationEntity vacation)
        {
            var temp = new VacationDTO
            {
                ID = vacation.ID,
                Type = vacation.Type,
                Duration = vacation.Duration,
                Date = vacation.Date,
                Status = vacation.Status,
                Note = vacation.Note,
                IsDraft = vacation.IsDraft,
                UserID = vacation.UserID
            };
            return temp;
        }

        public VacationEntity Map(VacationDTO vacation)
        {
            var temp = new VacationEntity
            {
                ID = vacation.ID,
                Type = vacation.Type,
                Duration = vacation.Duration,
                Date = vacation.Date,
                Status = vacation.Status,
                Note = vacation.Note,
                IsDraft = vacation.IsDraft,
                UserID = vacation.UserID,
                Created = DateTime.Now,
        };
            return temp;
        }


    }
}