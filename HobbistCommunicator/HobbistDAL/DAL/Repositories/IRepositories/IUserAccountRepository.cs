using Models.Models;
using Models.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories.IRepositories
{
    public interface IUserAccountRepository
    {
        AuthenticateResponse AuthenticateUser(LoginDetails loginDetails);
        bool ChangeEmail(LoginDetails loginDetails, string newEmail);
        string GetUserEmial(Guid id);
        string GetUserRole(Guid id);
        bool IsUserEmailAvailable(string email);
        UserAccount RegisterUser(UserAccount userAccount);
        bool RemoveUser(LoginDetails loginDetails);
        bool UpdateUserDateOfBirth(LoginDetails loginDetails, DateTime dateOfBirth);
        bool AddProfileToAccount(UserProfile userProfile, Guid userAccountId);
        bool IsUserBlocked(Guid id);
        UserAccount GetUserById(Guid id);
        DateTime GetUserDateOfBirth(Guid id);
        bool BlockOrUnblockUser(string userName, bool isBlocked);
        public bool Save();

    }
}
