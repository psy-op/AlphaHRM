using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaHRM.Models
{
    public class Enums
    {
        public enum ErrorCodes
        {
            Success = 0,
            UserNotFound = 10,
            VacationNotFound = 20,
            Unexpected = 30
        }


        public class UserType
        {
            public const bool Manager = true;
            public const bool User = false;
        }

        public enum VacationType
        {
            AnnualVacation = 0,
            SickVacation=1,
            LeaveVacation=2,
            Exceptional=3
        }

        public enum VacationStatus
        {
            UnderReview = 0,
            Approved = 1,
            Rejected = 2
        }

        public class isDraft
        {
            public const bool Draft = true;
            public const bool Submitted = false;
        }
    }
}
