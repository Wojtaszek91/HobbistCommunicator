using DAL.Repositories.IRepositories;
using HobbistCommunicator.MessageService.Interfaces;
using Microsoft.Extensions.Logging;
using Models.Models;
using Models.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbistCommunicator.MessageService
{
    public class MessageService : IMessageService
    {
        private readonly IUserMessageRepository _messageRepo;
        private readonly ILogger<MessageService> _logger;

        public MessageService(IUserMessageRepository messageRepo, ILogger<MessageService> logger)
        {
            _messageRepo = messageRepo;
            _logger = logger;
        }
        public async Task<Dictionary<UserIdUserNameModel, List<UserMessage>>> GetNotOpenUserMessagesMappedDictionary(Guid profileId)
        {
            try
            {
                var messageList = await _messageRepo.GetNotSendUserMessages(profileId);
                if (messageList.Count() > 0)
                    return GetMappedUserMessages(messageList);
                else
                    return null;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while trying to get not open messages from profileId: {profileId}, message: {e.Message}");
                return null;
            }
        }

        public async Task<Dictionary<UserIdUserNameModel, List<UserMessage>>> GetUserMessagesMappedDictionaryAtLogin(Guid profileId)
        {
            try
            {
                return GetMappedUserMessages(await _messageRepo.GetUserMessagesAtLogin(profileId));
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while trying to get messages by index from profileId: {profileId}. Message: {e.Message}");
                return null;
            }
        }

        public async Task<bool?> MarkMessageAsOpen(Guid messageId)
        {
            try
            {
                return await _messageRepo.MarkAsOpened(messageId);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while trying to mark message as readed. MessageId: {messageId}, message: {e.Message}");
                return null;
            }
        }

        public Task<bool> SaveNewMessage(UserMessage userMessage)
        {
            try
            {
                return _messageRepo.SaveMessage(userMessage);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while trying to save userMessage senderId: {userMessage.SenderProfileId} TargetProfileId: {userMessage.TargetProfileId}, message: {e.Message}");
                return null;
            }
        }

        private Dictionary<UserIdUserNameModel, List<UserMessage>> GetMappedUserMessages(IEnumerable<UserMessage> messagesList)
        {
            Dictionary<UserIdUserNameModel, List<UserMessage>> mappedDictionary = new Dictionary<UserIdUserNameModel, List<UserMessage>>();
            
            foreach(var message in messagesList)
            {
                AddMessageToDictionary(mappedDictionary, message);
            }

            return mappedDictionary;
        }

        private void AddMessageToDictionary(Dictionary<UserIdUserNameModel, List<UserMessage>> dictionary, UserMessage message)
        {
            UserIdUserNameModel idNameModel = new UserIdUserNameModel(message.TargetProfileId, message.TargetUserName);
            if (dictionary.ContainsKey(idNameModel))
            {
                dictionary[idNameModel].Add(message);
            }
            else
            {
                List<UserMessage> newMessageList = new List<UserMessage>();
                newMessageList.Add(message);
                dictionary.Add(idNameModel, newMessageList);
            }
        }
    }
}
