using CoachBot.Domain.Model;
using CoachBot.Domain.Services;
using CoachBot.Models;
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
    [ApiController]
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

        [HttpGet("{caseId}")]
        public IActionResult GetCase(int caseId)
        {
            if (!CanAccessCase(caseId))
            {
                return Unauthorized();
            }

            return Ok(_caseService.GetCase(caseId));
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

        [HubRolePermission(HubRole = PlayerHubRole.Player)]
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

        [HubRolePermission(HubRole = PlayerHubRole.Player)]
        [HttpPost]
        public IActionResult CreateCase(CreateCaseDto caseToCreate)
        {
            var caseId = _caseService.CreateCase(caseToCreate.Title, caseToCreate.Description, caseToCreate.CaseType, caseToCreate.Images);

            return Ok(caseId);
        }

        [HubRolePermission(HubRole = PlayerHubRole.Manager)]
        [HttpPut("{caseId}")]
        public IActionResult UpdateCase(int caseId, [FromBody]Case caseToUpdate)
        {
            _caseService.UpdateCase(caseToUpdate);

            return Ok();
        }

        [HubRolePermission(HubRole = PlayerHubRole.Player)]
        [HttpPost("{caseId}/notes")]
        public IActionResult CreateCaseNote(int caseId, [FromBody]CreateCaseNoteDto caseNote)
        {
            if (!CanAccessCase(caseId))
            {
                return Unauthorized();
            }

            _caseService.CreateCaseNote(caseId, caseNote.Text, caseNote.Images);

            return Ok();
        }

        [HubRolePermission(HubRole = PlayerHubRole.Player)]
        [HttpGet("{caseId}/notes")]
        public IActionResult GetCaseNotes(int caseId)
        {
            if (!CanAccessCase(caseId)) {
                return Unauthorized();
            }

            return Ok(_caseService.GetNotesForCase(caseId));
        }

        private bool CanAccessCase(int caseId)
        {
            var steamId = User.GetSteamId();
            var player = _playerService.GetPlayerBySteamId(User.GetSteamId());
            var currentCase = _caseService.GetCase(caseId);

            if (!_playerService.IsManagerOrAbove(steamId) && !(currentCase.CreatedById == player.Id))
            {
                return false;
            }

            return true;
        }

    }
}