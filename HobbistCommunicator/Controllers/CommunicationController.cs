using HobbistCommunicator.MessageService.Interfaces;
using HobbistCommunicator.RequestsModels;
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

        [HttpPost("CreateNewMessageBox")]
        public async Task<IActionResult> CreateNewMessageBox([FromBody] NewMessageBoxRequestModel requestModel)
        {
            var newMessageBoxId = await _messageService.CreateNewMessageBox(requestModel.ProfileOneId, requestModel.ProfileTwoId);
            if (newMessageBoxId == Guid.Empty) return BadRequest();
            return Ok(newMessageBoxId);
        }

        [HttpGet("GetUserAllMessageBoxes")]
        public async Task<IActionResult> GetUserAllMessageBoxes(Guid profileId)
        {
            var messageBoxList = await _messageService.GetAllMessageBoxes(profileId);
            return messageBoxList != null ? Ok(messageBoxList) : BadRequest();
        }
    }
}
