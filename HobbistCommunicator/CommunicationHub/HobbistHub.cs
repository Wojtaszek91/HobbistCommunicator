using HobbistCommunicator.MessageService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Models.Models;
using Newtonsoft.Json;
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
        public async Task SendMessageAsync(Guid messageBoxId, string message, Guid senderProfileId, Guid targetProfileId)
        {
            try
            {
                var userMessage = await _messageService.SaveNewMessage(messageBoxId, message, senderProfileId);
                if(userMessage != null)
                {
                    userMessage.MessageBox = null;
                    string targetConnectionId = "";
                    string senderConnectionId = "";
                    _connectedUsers.TryGetValue(_messageService.GetUsernameByProfileId(targetProfileId).Result.ToUpper(), out targetConnectionId);
                    _connectedUsers.TryGetValue(_messageService.GetUsernameByProfileId(senderProfileId).Result.ToUpper(), out senderConnectionId);
                    if (targetConnectionId != null)
                        await Clients.Client(targetConnectionId).SendAsync("ReciveMessage", messageBoxId, userMessage);
                    if (senderConnectionId != null)
                        await Clients.Client(senderConnectionId).SendAsync("ReciveMessage", messageBoxId, userMessage);
                }
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
