using Models.Models;
using Models.Models.DTOs;
using Models.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbistCommunicator.MessageService.Interfaces
{
    public interface IMessageService
    {
        Task<List<MessageBox>> GetAllMessageBoxes(Guid profileId);
        Task<UserMessage> SaveNewMessage(Guid messageBoxId, string content, Guid senderProfileId);
        Task<Guid> CreateNewMessageBox(Guid profileOneId, Guid profileTwoId);
        Task<MessageBox> GetMessageBoxById(Guid id);
        Task<string> GetUsernameByProfileId(Guid profileId);
    }
}
