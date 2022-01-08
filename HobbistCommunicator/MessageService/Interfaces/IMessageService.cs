using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbistCommunicator.MessageService.Interfaces
{
    public interface IMessageService
    {
        Task<IEnumerable<UserMessage>> GetNotOpenUserMessages(Guid profileId);
        Task<IEnumerable<UserMessage>> GetUserMessagesByIndex(Guid profileId, int index);
        Task<bool> SaveNewMessage(UserMessage userMessage);
        Task<bool?> MarkMessageAsOpen(Guid messageId);
    }
}
