using CoachBot.Domain.Model;
using CoachBot.Domain.Services;
using CoachBot.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CoachBot.Attributes.HubRoleAuthorizeAttribute;

namespace CoachBot.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BanController : Controller
    {
        private readonly BanService _banService;
        private readonly DiscordNotificationService _discordNotificationService;

        public BanController(BanService banService, DiscordNotificationService discordNotificationService)
        {
            _banService = banService;
            _discordNotificationService = discordNotificationService;
        }

        [HubRolePermission(HubRole = PlayerHubRole.Manager)]
        [HttpPost]
        public async Task<IActionResult> CreateBan(Ban banToCreate)
        {
            await _banService.CreateBan(banToCreate);

            return Ok();
        }

        [HubRolePermission(HubRole = PlayerHubRole.Manager)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBan(Ban banToUpdate)
        {
            await _banService.UpdateBan(banToUpdate);

            return Ok();
        }

        [HubRolePermission(HubRole = PlayerHubRole.Manager)]
        [HttpGet("{id}")]
        public Ban GetBan(int id)
        {
            return _banService.GetBans().First(b => b.Id == id);
        }

        [HubRolePermission(HubRole = PlayerHubRole.Manager)]
        [HttpGet]
        public List<Ban> GetBans()
        {
            return _banService.GetBans();
        }

        [HubRolePermission(HubRole = PlayerHubRole.Administrator)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBan(int id)
        {
            await _banService.DeleteBan(id);

            return Ok();
        }

        [HubRolePermission(HubRole = PlayerHubRole.Manager)]
        [HttpPost("update-ban-message")]
        public async Task<IActionResult> SendBanChannelMessage(BanChannelMessageDto banMessageDto)
        {
            await _discordNotificationService.SendBanChannelMessage(banMessageDto.Message);

            return Ok();
        }

        [HubRolePermission(HubRole = PlayerHubRole.Manager)]
        [HttpGet("get-ban-message")]
        public async Task<BanChannelMessageDto> GetBanChannelMessage()
        {
            var message = await _discordNotificationService.GetBanChannelMessageText();

            return new BanChannelMessageDto() { Message = message };
        }

    }
}
