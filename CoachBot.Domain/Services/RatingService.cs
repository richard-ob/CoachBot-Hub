using CoachBot.Database;
using CoachBot.Domain.Model;
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

        public List<PlayerRatingSnapshot> GetAllPlayerRatings()
        {
            return _coachBotContext
                    .PlayerRatingSnapshots
                    .FromSqlInterpolated($@"SELECT  Players.Id as Id,
                                                Players.SteamID as SteamID,
                                                Players.Name as Name,
                                                Players.Rating as Rating,
                                                PlayerTeams.Name as TeamName,
                                                PlayerTeams.TeamRole as TeamRole                                        
                                        FROM dbo.Players Players
                                        CROSS APPLY 
                                        (
	                                        SELECT PlayerTeams.TeamRole, Teams.Name
	                                        FROM dbo.PlayerTeams PlayerTeams	
	                                        INNER JOIN dbo.Teams Teams
		                                        ON Teams.Id = PlayerTeams.TeamId AND Teams.TeamType = 1 AND Teams.RegionId = 1
                                        WHERE PlayerTeams.PlayerId = Players.Id AND PlayerTeams.IsPending = 0 AND PlayerTeams.LeaveDate IS NULL
                                        ) PlayerTeams
                                        WHERE Players.Rating > 0
                                        ORDER BY PlayerTeams.Name")
                   .ToList();
        }

        public List<PlayerRatingSnapshot> GetRateablePlayers()
        {
            return _coachBotContext
                    .PlayerRatingSnapshots
                    .FromSqlInterpolated($@"SELECT  Players.Id as Id,
                                                Players.SteamID as SteamID,
                                                Players.Name as Name,
                                                Players.Rating as Rating,
                                                PlayerTeams.Name as TeamName,
                                                PlayerTeams.TeamRole as TeamRole                                        
                                        FROM dbo.Players Players
                                        OUTER APPLY 
                                        (
	                                        SELECT PlayerTeams.TeamRole, Teams.Name
	                                        FROM dbo.PlayerTeams PlayerTeams	
	                                        INNER JOIN dbo.Teams Teams
		                                        ON Teams.Id = PlayerTeams.TeamId AND Teams.TeamType = 1 AND Teams.RegionId = 1
                                        WHERE PlayerTeams.PlayerId = Players.Id AND PlayerTeams.IsPending = 0 AND PlayerTeams.LeaveDate IS NULL
                                        ) PlayerTeams
                                        WHERE PlayerTeams.Name IS NOT NULL OR Players.Rating > 0
                                        ORDER BY PlayerTeams.Name")
                   .ToList();
        }
    }
}
