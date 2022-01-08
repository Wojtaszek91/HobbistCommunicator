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
        Task<bool> MarkAsReaded(Guid messageId);
        Task<IEnumerable<UserMessage>> GetUserMessages(Guid userProfileId, int index);
        Task<IEnumerable<UserMessage>> GetNotOpenUserMessages(Guid userProfileId);
    }
}
