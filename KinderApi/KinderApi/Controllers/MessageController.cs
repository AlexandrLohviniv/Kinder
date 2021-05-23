using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using KinderApi.DTOs;
using KinderApi.Models;
using KinderApi.ServiceProtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KinderApi.Controllers
{
    [ApiController]
    [Route("[controller]/{userId}")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService messageService;
        private readonly IMapper mapper;

        public MessageController(IMessageService messageService, IMapper mapper)
        {
            this.messageService = messageService;
            this.mapper = mapper;
        }

        [HttpPost("sendMessage/{toId}")]
        public async Task<IActionResult> SendMessage(int userId, int toId, [FromBody] object text)
        {
            var messagePattern = new {text = ""};
            var message =  JsonConvert.DeserializeAnonymousType(text.ToString(), messagePattern);

            await messageService.SendMessage(userId, toId, message.text);

            return Ok();
        }

        [HttpGet("messageThread/{toId}")]
        public async Task<IActionResult> GetMessageThread(int userId, int toId)
        {
            List<Message> messages = await messageService.GetMesageThread(userId, toId);

            if (messages == null)
                return BadRequest();

            List<MessageDto> messagesToReturn = mapper.Map<List<MessageDto>>(messages);

            return Ok(messagesToReturn);
        }
    }
}