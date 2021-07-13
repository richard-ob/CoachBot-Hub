using CoachBot.Database;
using CoachBot.Domain.Model;
using CoachBot.Model;
using System.Linq;

namespace CoachBot.Domain.Extensions
{
    public static class PlayerQueryExtensions
    {
        private static int[] BANNED_PLAYERS = new int[] { 545 };

        public static Player GetPlayerBySteamId(this CoachBotContext coachBotContext, ulong steamUserId)
        {
            return coachBotContext.Players.Single(p => p.SteamID == steamUserId);
        }

        public static IQueryable<Player> ExcludeBannedPlayers(this IQueryable<Player> playerQueryable)
        {
            return playerQueryable.Where(p => !BANNED_PLAYERS.Any(bp => bp == p.Id));
        }

        public static IQueryable<PlayerPositionMatchStatistics> ExcludeBannedPlayers(this IQueryable<PlayerPositionMatchStatistics> playerQueryable)
        {
            return playerQueryable.Where(p => !BANNED_PLAYERS.Any(bp => bp == p.PlayerId));
        }

        public static IQueryable<PlayerMatchStatistics> ExcludeBannedPlayers(this IQueryable<PlayerMatchStatistics> playerQueryable)
        {
            return playerQueryable.Where(p => !BANNED_PLAYERS.Any(bp => bp == p.PlayerId));
        }
    }
}