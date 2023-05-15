using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignalRChatTemplete.Models.DTOs;
using SignalRTemplete.Hubs.interfaces;
using System.Data;

namespace SignalRChatTemplete.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatHubController : ControllerBase
    {
        private readonly IChatHub _chatHub;

        public ChatHubController(IChatHub chatHub)
        {
            _chatHub = chatHub;
        }

        [HttpPost]
        [Route("SendGlobeMessage")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult SendGlobeMessage(SendMessageDTO input)
        {
            _chatHub.SendGlobeMessage(input);
            return Ok();
        }

        [HttpPost]
        [Route("SendGroupMessage")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult SendGroupMessage(SendGroupMessageDTO input)
        {
            _chatHub.SendGroupMessage(input);
            return Ok();
        }

        [HttpPost]
        [Route("SendPrivateMessage")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult SendPrivateMessage(SendPrivateMessageDTO input)
        {
            _chatHub.SendPrivateMessage(input);
            return Ok();
        }

        [HttpPost]
        [Route("JoinGroup")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult JoinGroup(int UserID, int ToUserID)
        {
            _chatHub.JoinGroup(UserID, ToUserID);
            return Ok();
        }

        [HttpPost]
        [Route("LeaveGroup")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult LeaveGroup(int UserID, int ToUserID)
        {
            _chatHub.LeaveGroup(UserID, ToUserID);
            return Ok();
        }
    }
}
