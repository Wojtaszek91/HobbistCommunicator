using HobbistCommunicator.MessageService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HobbistCommunicator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommunicationController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly ILogger<CommunicationController> _logger;

        public CommunicationController(IMessageService messageService, ILogger<CommunicationController> logger)
        {
            _messageService = messageService;
            _logger = logger;
        }

        [HttpPost("SendMessageToUser")]
        public async Task<IActionResult> SendMessageToUser([FromBody] UserMessage userMessage)
        {
            if (userMessage.SenderProfileId == Guid.Empty || userMessage.TargetProfileId == Guid.Empty || string.IsNullOrEmpty(userMessage.Content))
                return BadRequest("Wrong Content");

            return await _messageService.SaveNewMessage(userMessage) == true ?  Ok() : BadRequest("Inner exception");
        }

        [HttpPost("MarkAsOpen")]
        public async Task<IActionResult> MarkAsOpen([FromBody] Guid MessageId)
        {
            if (MessageId == Guid.Empty) 
                return BadRequest("Didn't recive id");

            var result = await _messageService.MarkMessageAsOpen(MessageId);

            if (result == null || result == false)
                return BadRequest("Coudn't mark as open");

            return NoContent();
        }

        [HttpGet("GetMessagesByIndex")]
        public async Task<IActionResult> GetMessagesByIndex(Guid profileId, int index)
        {
            var messageList =  await _messageService.GetUserMessagesByIndex(profileId, index);

            if (messageList == null)
                return BadRequest("Coudn't get message list");

            return Ok(messageList);
        }

        [HttpGet("OnlineMessageSubscribe")]
        public async Task SubscribeForNewMessages(Guid profileId)
        {
            List<Guid> alreadySendMessages = new List<Guid>();
            List<UserMessage> messageToSend = new List<UserMessage>();

            var response = Response;
            response.Headers.Add("Content-Type", "text/event-stream");
            response.StatusCode = 200;

            while (true)
            {
                var messageList = await _messageService.GetNotOpenUserMessages(profileId);

                if (messageList != null)
                {
                    foreach(var message in messageList)
                    {
                        if (!alreadySendMessages.Contains(message.Id))
                        {
                            messageToSend.Add(message);
                        }
                    }

                    await response.WriteAsync($"{PrepareStreamMessage(messageToSend)}");
                    response.Body.FlushAsync();

                    messageToSend.ForEach(x => alreadySendMessages.Add(x.Id));
                    messageToSend.Clear();
                    Thread.Sleep(3000);
                }
            }
        }      

        private string PrepareStreamMessage(List<UserMessage> messageList)
        {
            var messageListStringObject = JsonConvert.SerializeObject(messageList);

            return JsonConvert.SerializeObject(
                new JObject(
                    new JProperty("messageList", messageListStringObject),
                    new JsonSerializerSettings
                    { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
        }
    }
}
