using DAL.Repositories;
using DAL.Repositories.IRepositories;
using HobbistCommunicator.MessageService.Interfaces;
using Microsoft.Extensions.Logging;
using Models.Models;
using Models.Models.DTOs;
using Models.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbistCommunicator.MessageService
{
    public class MessageService : IMessageService
    {
        private readonly UserMessageRepository _messageRepo;
        //private readonly ILogger<MessageService> _logger;

        public MessageService(UserMessageRepository messageRepo)
        {
            _messageRepo = messageRepo;
            //_logger = logger;
        }

        public async Task<Guid> CreateNewMessageBox(Guid profileOneId, Guid profileTwoId)
        {
            try
            {
                var newMessageBoxId = await _messageRepo.CreateNewMessageBox(profileOneId, profileTwoId);
                if (newMessageBoxId == Guid.Empty) return Guid.Empty;
                return newMessageBoxId;
            }
            catch(Exception e)
            {
                //_logger.LogError($"Error while trying to create new MessageBox. ProfileOneId: {profileOneId} ProfileTwoId: {profileTwoId}. Message: {e.Message}");
                return Guid.Empty;
            }
        }

        public async Task<UserMessage> SaveNewMessage(Guid messageBoxId, string content, Guid senderProfileId)
        {
            try
            {
                return await _messageRepo.SaveNewMessage(messageBoxId, content, senderProfileId);
            }
            catch(Exception e)
            {
                //_logger.LogError($"Error while trying to save new message. MessageBoxId: {messageBoxId}, SenderProfileId: {senderProfileId}");
                return null;
            }
        }

        public async Task<MessageBox> GetMessageBoxById(Guid id)
        {
            try
            {
                var messageBox = await _messageRepo.GetMessageBoxById(id);
                return messageBox;
            }
            catch(Exception e)
            {
                //_logger.LogError($"Error while trying to get MessageBox id: {id}. Message: {e.Message}");
                return null;
            }
        }

        public Task<List<MessageBox>> GetAllMessageBoxes(Guid profileId)
        {
            try
            {
                return _messageRepo.GetAllUserMessageBoxes(profileId);
            }
            catch(Exception e)
            {
                //_logger.LogError($"Error while trying to get MessageBox list for profile id: {profileId}. Message: {e.Message}");
                return null;
            }
        }

        public Task<string> GetUsernameByProfileId(Guid profileId)
        {
            return _messageRepo.GetUsernameByProfileId(profileId);
        }
    }
}
