using HobbistCommunicator.MessageService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Models;
using Models.Models.DTOs;
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
        public async Task<IActionResult> SendMessageToUser([FromBody] NewUserMessageDto userMessage)
        {
            if (userMessage.SenderProfileId == Guid.Empty
                || userMessage.TargetProfileId == Guid.Empty
                || string.IsNullOrEmpty(userMessage.Content)
                || string.IsNullOrEmpty(userMessage.TargetUserName))
                return BadRequest("Wrong Content");

            UserMessage newUserMessage = new UserMessage(userMessage.SenderProfileId, userMessage.TargetProfileId, userMessage.Content, userMessage.TargetUserName);

            return await _messageService.SaveNewMessage(newUserMessage) == true ? Ok() : BadRequest("Inner exception");
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

        [HttpGet("GetMessagesAtLogin")]
        public async Task<IActionResult> GetMessagesAtLogin(Guid profileId)
        {
            var messageList = await _messageService.GetUserMessagesMappedDictionaryAtLogin(profileId);

            if (messageList == null)
                return BadRequest("Coudn't get message list");

            return Ok(messageList);
        }

        [HttpGet("OnlineMessageSubscribe")]
        public async Task SubscribeForNewMessages(Guid profileId)
        {
            var response = Response;
            response.Headers.Add("Content-Type", "text/event-stream");
            response.StatusCode = 200;

            while (true)
            {
                var messagesMappedDictionary = await _messageService.GetNotOpenUserMessagesMappedDictionary(profileId);

                if (messagesMappedDictionary != null)
                {
                    await response.WriteAsync($"{PrepareStreamMessage(messagesMappedDictionary)}");
                    response.Body.FlushAsync();
                }
                Thread.Sleep(3000);
            }
        }

        private string PrepareStreamMessage<T>(T messageList)
            => JsonConvert.SerializeObject(new JObject(
                new JProperty("messageList", JsonConvert.SerializeObject(messageList))),
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
    }
}
