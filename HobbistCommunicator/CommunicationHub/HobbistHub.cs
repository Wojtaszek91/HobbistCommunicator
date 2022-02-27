using HobbistCommunicator.MessageService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbistCommunicator.CommunicationHub
{
    [Authorize]
    public class HobbistHub : Hub
    {
        private Dictionary<string, string> _connectedUsers = new Dictionary<string, string>();
        private readonly IMessageService _messageService;

        public HobbistHub(IMessageService messageService) : base()
        {
            _messageService = messageService;
        }

        public override Task OnConnectedAsync()
        {
            var userName = Context.GetHttpContext().User.Identity.Name.ToUpper();
            var connectionId = Context.ConnectionId;
            if (_connectedUsers.ContainsKey(userName)) _connectedUsers[userName] = connectionId;
            else _connectedUsers.Add(userName, connectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var userName = Context.GetHttpContext().User.Identity.Name.ToUpper();
            _connectedUsers.Remove(userName);
            return base.OnDisconnectedAsync(exception);
        }
        public async Task SendMessageAsync(string messageBoxId, string message, string senderProfileId, string targetUserName)
        {
            try
            {
                Guid messageBoxGuidId = Guid.Parse(messageBoxId);
                Guid senderProfileGuidId = Guid.Parse(senderProfileId);
                var result = await _messageService.SaveNewMessage(messageBoxGuidId, message, senderProfileGuidId);
                var targetConnectionId = _connectedUsers[targetUserName.ToUpper()];
                if (targetConnectionId != null)
                    await Clients.Client(targetConnectionId).SendAsync("ReciveMessage", messageBoxId, senderProfileId, message);
                
            }
            catch(Exception e)
            {

            }

        }

        public async Task SendToAll(string message)
        {
            await Clients.All.SendAsync(message);
        }
    }
}
