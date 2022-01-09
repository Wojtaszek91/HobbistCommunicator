﻿using DAL.Repositories.IRepositories;
using HobbistCommunicator.MessageService.Interfaces;
using Microsoft.Extensions.Logging;
using Models.Models;
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
        public async Task<IEnumerable<UserMessage>> GetNotOpenUserMessages(Guid profileId)
        {
            try
            {
                return await _messageRepo.GetNotOpenUserMessages(profileId);
            }
            catch(Exception e)
            {
                _logger.LogError($"Error while trying to get not open messages from profileId: {profileId}, message: {e.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<UserMessage>> GetUserMessagesByIndex(Guid profileId, int index)
        {
            try
            {
                return await _messageRepo.GetUserMessages(profileId, index);
            }
            catch(Exception e)
            {
                _logger.LogError($"Error while trying to get messages by index from profileId: {profileId} index: {index}, message: {e.Message}");
                return null;
            }
        }

        public async Task<bool?> MarkMessageAsOpen(Guid messageId)
        {
            try
            {
                return await _messageRepo.MarkAsReaded(messageId);
            }
            catch(Exception e)
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
            catch(Exception e)
            {
                _logger.LogError($"Error while trying to save userMessage senderId: {userMessage.SenderProfileId} TargetProfileId: {userMessage.TargetProfileId}, message: {e.Message}");
                return null;
            }
        }
    }
}