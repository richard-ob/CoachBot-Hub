using CoachBot.Domain.Model;
using CoachBot.Domain.Services;
using CoachBot.Shared.Extensions;
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
    [Authorize]
    public class CaseController : Controller
    {
        private readonly CaseService _caseService;
        private readonly PlayerService _playerService;

        public CaseController(CaseService caseService, PlayerService playerService)
        {
            _caseService = caseService;
            _playerService = playerService;
        }

        [HttpGet("@me")]
        public IActionResult GetMyCases()
        {
            var player = _playerService.GetPlayerBySteamId(User.GetSteamId());

            if (player == null)
            {
                return NotFound();
            }

            return Ok(_caseService.GetCasesForUser(player.Id));
        }

        [HubRolePermission(HubRole = PlayerHubRole.Manager)]
        [HttpGet("all")]
        public IActionResult GetCases()
        {
            return Ok(_caseService.GetAllCases());
        }

        [HubRolePermission(HubRole = PlayerHubRole.Manager)]
        [HttpGet("unassigned")]
        public IActionResult GetUnassignedCases()
        {
            return Ok(_caseService.GetUnassignedCases());
        }

        [HttpPut]
        public IActionResult CreateCase(Case caseToCreate)
        {
            var caseId = _caseService.CreateCase(caseToCreate);

            return Ok(caseId);
        }

        [HubRolePermission(HubRole = PlayerHubRole.Manager)]
        [HttpPost]
        public IActionResult UpdateCase(Case caseToUpdate)
        {
            _caseService.UpdateCase(caseToUpdate);

            return Ok();
        }

        [HttpPut("{caseId}/notes")]
        public IActionResult CreateCaseNote(int caseId, CaseNote caseNote)
        {
            var steamId = User.GetSteamId();
            var player = _playerService.GetPlayerBySteamId(User.GetSteamId());
            var currentCase = _caseService.GetCase(caseId);

            if (!_playerService.IsManagerOrAbove(steamId) && !(currentCase.CreatedById == player.Id))
            {
                return Unauthorized();
            }

            _caseService.CreateCaseNote(caseNote);

            return Ok();
        }

        [HttpGet("{caseId}/notes")]
        public IActionResult GetCaseNotes(int caseId)
        {
            var steamId = User.GetSteamId();
            var player = _playerService.GetPlayerBySteamId(User.GetSteamId());
            var currentCase = _caseService.GetCase(caseId);

            if (!_playerService.IsManagerOrAbove(steamId) && !(currentCase.CreatedById == player.Id))
            {
                return Unauthorized();
            }

            return Ok(_caseService.GetNotesForCase(caseId));
        }

    }
}