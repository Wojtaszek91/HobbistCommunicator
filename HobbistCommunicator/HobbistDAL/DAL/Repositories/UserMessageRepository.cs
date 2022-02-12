using DAL.DataContext;
using DAL.Repositories.IRepositories;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserMessageRepository : IUserMessageRepository
    {
        private readonly ApplicationDbContext _context;

        public UserMessageRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<UserMessage>> GetUserMessagesAtLogin(Guid userProfileId)
        {
            var messages = _context.UserMessages.
                Where(x => x.SenderProfileId == userProfileId)
                .OrderByDescending(x => x.SendTime);

            await MarkAsSend(messages);

            return messages;
        }

        public async Task<IEnumerable<UserMessage>> GetNotSendUserMessages(Guid userProfileId)
        {
            var messages = _context.UserMessages.
                Where(x => x.SenderProfileId == userProfileId && x.HasBeenSend == false)
                .OrderByDescending(x => x.SendTime);

            await MarkAsSend(messages);

            return messages;
        }

        public Task<bool> SaveMessage(UserMessage userMessage)
        {
            _context.UserMessages.AddAsync(userMessage);
            return Task.FromResult(SaveChanges());
        }

        public Task<bool> MarkAsOpened(Guid messageId)
        {
            _context.UserMessages.FirstOrDefault(x => x.Id == messageId).HasBeenOpen = true;
            return Task.FromResult(SaveChanges());
        }

        private async Task<bool> MarkAsSend(IEnumerable<UserMessage> messages)
        {
            try
            {
                foreach (var message in messages)
                {
                    if (_context.UserMessages.FirstOrDefault(x => x.Id == message.Id).HasBeenSend == false)
                        _context.UserMessages.FirstOrDefault(x => x.Id == message.Id).HasBeenSend = true;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        private bool SaveChanges() 
            => _context.SaveChanges() > 0 ? true : false;
    }
}
