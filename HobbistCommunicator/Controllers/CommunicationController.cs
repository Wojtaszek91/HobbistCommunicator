using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbistCommunicator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommunicationController : ControllerBase
    {
        private readonly ILogger<CommunicationController> _logger;

        public CommunicationController()
        {
        }

        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] AddHashtagNameDto newHashTagRequest)
        {
        }
    }
}
