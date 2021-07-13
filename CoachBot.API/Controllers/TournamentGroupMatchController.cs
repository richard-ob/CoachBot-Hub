using CoachBot.Domain.Model;
using CoachBot.Domain.Services;
using CoachBot.Extensions;
using CoachBot.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using static CoachBot.Attributes.HubRoleAuthorizeAttribute;

namespace CoachBot.Controllers
{
    [Produces("application/json")]
    [Route("api/tournament-group-matches")]
    [ApiController]
    public class TournamentGroupMatchController : Controller
    {
        private readonly TournamentService _tournamentService;
        private readonly PlayerService _playerService;

        public TournamentGroupMatchController(TournamentService tournamentService, PlayerService playerService)
        {
            _tournamentService = tournamentService;
            _playerService = playerService;
        }

        [HttpGet("{matchId}")]
        public TournamentGroupMatch GetTournamentGroupMatchForMatch(int matchId)
        {
            return _tournamentService.GetTournamentGroupMatchForMatch(matchId);
        }

        [HttpPut]
        public IActionResult UpdateTournamentGroupMatch(TournamentGroupMatch tournamentGroupMatch)
        {
            var tournamentId = _tournamentService.GetTournamentIdForGroupMatch(tournamentGroupMatch.Id);
            if (!_tournamentService.IsTournamentOrganiser(tournamentId, User.GetSteamId()) && !_playerService.IsAdminOrOwner(User.GetSteamId()))
            {
                return Unauthorized();
            }

            _tournamentService.UpdateTournamentGroupMatch(tournamentGroupMatch);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTournamentGroupMatch(int id)
        {
            var tournamentId = _tournamentService.GetTournamentIdForGroupMatch(id);
            if (!_tournamentService.IsTournamentOrganiser(tournamentId, User.GetSteamId()) && !_playerService.IsAdminOrOwner(User.GetSteamId()))
            {
                return Unauthorized();
            }

            _tournamentService.DeleteTournamentGroupMatch(id);

            return Ok();
        }

        [HttpPost("{tournamentId}")]
        public IActionResult CreateTournamentGroupMatch([FromBody]TournamentGroupMatch tournamentGroupMatch, int tournamentId)
        {
            if (tournamentId == 0)
            {
                return BadRequest();
            }

            if (!_tournamentService.IsTournamentOrganiser(tournamentId, User.GetSteamId()) && !_playerService.IsAdminOrOwner(User.GetSteamId()))
            {
                return Unauthorized();
            }

            _tournamentService.CreateTournamentGroupMatch(tournamentGroupMatch);

            return Ok();
        }

    }
}