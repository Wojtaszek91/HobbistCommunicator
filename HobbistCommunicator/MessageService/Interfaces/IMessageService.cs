using Models.Models;
using Models.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbistCommunicator.MessageService.Interfaces
{
    public interface IMessageService
    {
        Task<Dictionary<UserIdUserNameModel, List<UserMessage>>> GetNotOpenUserMessagesMappedDictionary(Guid profileId);
        Task<Dictionary<UserIdUserNameModel, List<UserMessage>>> GetUserMessagesMappedDictionaryAtLogin(Guid profileId);
        Task<bool> SaveNewMessage(UserMessage userMessage);
        Task<bool?> MarkMessageAsOpen(Guid messageId);
    }
}
