namespace AlphaHRM.Models
{
    public class Enums
    {
        public enum ErrorCodes
        {
            Success = 0,
            UserNotFound = 10,
            VacationNotFound = 20,
            Unexpected = 30,
            InvalidLogin = 40,
            ExistingUser = 50,
            ServerError = 60,
        }


        public enum UserType
        {
            User = 0,
            Manager = 1,

        }

        public enum VacationType
        {
            AnnualVacation = 0,
            SickVacation = 1,
            LeaveVacation = 2,
            Exceptional = 3
        }

        public enum VacationStatus
        {
            UnderReview = 0,
            Approved = 1,
            Rejected = 2
        }

        public enum Drafted
        {
            Draft = 0,
            Submitted = 1
        }
    }
}
