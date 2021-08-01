using CoachBot.Database;
using CoachBot.Domain.Helpers;
using CoachBot.Domain.Model;
using CoachBot.Shared.Extensions;
using CoachBot.Shared.Helpers;
using Discord.Rest;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachBot.Domain.Services
{
    public class BanService
    {
        private readonly CoachBotContext _dbContext;
        private readonly MySqlConnection _mySqlConnection;
        private readonly DiscordRestClient _discordRestClient;
        private readonly DiscordNotificationService _discordNotificationService;

        public BanService(CoachBotContext dbContext, MySqlConnection mySqlConnection, DiscordRestClient discordRestClient, DiscordNotificationService discordNotificationService)
        {
            _dbContext = dbContext;
            _mySqlConnection = mySqlConnection;
            _discordRestClient = discordRestClient;
            _discordNotificationService = discordNotificationService;
        }

        public async Task CreateBan(Ban ban)
        {
            var player = _dbContext.Players.Single(s => s.Id == ban.BannedPlayerId);

            if (player.HubRole != PlayerHubRole.Player) return;

            var steamId = SteamIdHelper.ConvertSteamID64ToSteamID((ulong)player.SteamID);

            _dbContext.Bans.Add(ban);
            _dbContext.SaveChanges();

            _mySqlConnection.OpenAsync().Wait();

            var banStartTicks = ((DateTimeOffset)ban.StartDate.Date).ToUnixTimeSeconds();
            var banEndTicks = ban.EndDate.HasValue ? ((DateTimeOffset)ban.EndDate.Value.Date).ToUnixTimeSeconds() : banStartTicks;
            var banLength = banEndTicks - banStartTicks;
            var banId = _dbContext.Entry(ban).Property(m => m.Id).CurrentValue; 

            if (ban.BanType == BanType.Community)
            {
                var sqlQuery = 
                    $"INSERT INTO iosdb.sb_bans(ip, authid, `name`, created, ends, length, reason, aid, adminip, sid,  `type`)" +
                    $"VALUES('', '{steamId}', '{player.Name}', '{banStartTicks}', '{banEndTicks}', {banLength}, 'IOSocer Hub Ban ID {banId}', 1, '101.101.101.101', 0, 0);";

                using (var command = new MySqlCommand(sqlQuery, _mySqlConnection))
                {
                    command.ExecuteNonQuery();
                    ban.SourceBansId = command.LastInsertedId;
                }

                _dbContext.SaveChanges();
            }

            if (player.DiscordUserId.HasValue)
            {
                var guild = await _discordRestClient.GetGuildAsync(ConfigHelper.GetConfig().DiscordConfig.OwnerGuildId);
                await guild.AddBanAsync((ulong)player.DiscordUserId, 0, $"IOSoccer Hub Ban ID {banId}");
            }

            _mySqlConnection.Close();

            // TODO: Send ban DM
            var playerId = CallContext.GetData(CallContextDataType.PlayerId);
            var playerCreated = _dbContext.Players.Find(playerId);
            await _discordNotificationService.SendModChannelMessage($"{player.Name} banned by {playerCreated.Name} for {ban.BanDuration} - http://www.iosoccer.com/ban/{banId}", "Ban Created");
        }

        public async Task UpdateBan(Ban ban)
        {
            var player = _dbContext.Players.Single(s => s.Id == ban.BannedPlayerId);

            if (player.HubRole != PlayerHubRole.Player) return;

            var steamId = SteamIdHelper.ConvertSteamID64ToSteamID((ulong)player.SteamID);
            var existingBan = _dbContext.Bans.Single(b => b.Id == ban.Id);

            existingBan.StartDate = ban.StartDate;
            existingBan.EndDate = ban.EndDate;
            existingBan.BanReason = ban.BanReason;
            existingBan.BanType = ban.BanType;
            existingBan.BanInfo = ban.BanInfo;

            _dbContext.SaveChanges();

            _mySqlConnection.OpenAsync().Wait();

            var banStartTicks = ((DateTimeOffset)ban.StartDate.Date).ToUnixTimeSeconds();
            var banEndTicks = ban.EndDate.HasValue ? ((DateTimeOffset)ban.EndDate.Value.Date).ToUnixTimeSeconds() : banStartTicks;
            var banLength = banEndTicks - banStartTicks;
            var banId = _dbContext.Entry(ban).Property(m => m.Id).CurrentValue;

            var sqlQuery =
                $"UPDATE iosdb.sb_bans" +
                $"SET created = '{banStartTicks}', ends = '{banEndTicks}', length = {banLength}, reason = 'IOSocer Hub Ban ID {banId}'" +
                $"WHERE bid = {existingBan.Id};";

            using (var command = new MySqlCommand(sqlQuery, _mySqlConnection))
            {
                command.ExecuteNonQuery();
            }

            if (player.DiscordUserId.HasValue && existingBan.EndDate.HasValue && existingBan.EndDate < DateTime.Now)
            {
                var guild = await _discordRestClient.GetGuildAsync(ConfigHelper.GetConfig().DiscordConfig.OwnerGuildId);
                await guild.RemoveBanAsync((ulong)player.DiscordUserId);
            }

            _dbContext.SaveChanges();
            _mySqlConnection.Close();

            var playerId = CallContext.GetData(CallContextDataType.PlayerId);
            var playerUpdated = _dbContext.Players.Find(playerId);
            await _discordNotificationService.SendModChannelMessage($"{player.Name}'s ban updated by {playerUpdated.Name} - http://www.iosoccer.com/ban/{banId}", "Ban Updated");
        }

        public async Task DeleteBan(int banId)
        {
            var ban = _dbContext.Bans.Single(b => b.Id == banId);
            var player = _dbContext.Players.Single(s => s.Id == ban.BannedPlayerId);
            var steamId = player.SteamID;

            _mySqlConnection.OpenAsync().Wait();

            var sqlQuery =
                $"DELETE FROM iosdb.sb_bans" +
                $"WHERE bid = {banId};";

            using (var command = new MySqlCommand(sqlQuery, _mySqlConnection))
            {
                command.ExecuteNonQuery();
            }

            if (player.DiscordUserId.HasValue)
            {
                var guild = await _discordRestClient.GetGuildAsync(ConfigHelper.GetConfig().DiscordConfig.OwnerGuildId);
                try
                {
                    await guild.RemoveBanAsync((ulong)player.DiscordUserId);
                }
                catch
                {

                }
            }
            _dbContext.Bans.Remove(_dbContext.Bans.Single(s => s.Id == banId));

            _dbContext.SaveChanges();

            _mySqlConnection.Close();
        }

        public List<Ban> GetBans()
        {
            return _dbContext.Bans
                .Include(b => b.BannedPlayer)
                .OrderByDescending(b => b.CreatedDate)
                .ToList();
        }

    }
}
