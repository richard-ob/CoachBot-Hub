using CoachBot.Database;
using CoachBot.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachBot.Domain.Services
{
    public class RatingService
    {
        private readonly CoachBotContext _coachBotContext;

        public RatingService(CoachBotContext coachBotContext)
        {
            _coachBotContext = coachBotContext;
        }

        public List<Player> GetAllPlayerRatings()
        {
            return _coachBotContext.Players
                .AsQueryable().Where(p => p.Rating > 0)
                .ToList();
        }

        public List<Player> GetRateablePlayers()
        {
            return _coachBotContext.Players
                .AsQueryable()
                .Where(p => p.Rating > 0 || _coachBotContext.PlayerTeams.Any(pt => pt.IsPending == false && pt.LeaveDate == null && pt.PlayerId == p.Id && pt.Team.RegionId == 1 && pt.Team.TeamType == Model.TeamType.Club))
                .ToList();         
        }
    }
}
