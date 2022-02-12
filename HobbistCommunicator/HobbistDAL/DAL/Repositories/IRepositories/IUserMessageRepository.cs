using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.IRepositories
{
    public interface IUserMessageRepository
    {
        Task<bool> SaveMessage(UserMessage userMessage);
        Task<bool> MarkAsOpened(Guid messageId);
        Task<IEnumerable<UserMessage>> GetUserMessagesAtLogin(Guid userProfileId);
        Task<IEnumerable<UserMessage>> GetNotSendUserMessages(Guid userProfileId);
    }
}
