using CoachBot.Model;
using CoachBot.Shared.Model;
using CoachBot.Tools;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoachBot.Domain.Services
{
    public class DiscordNotificationService
    {
        private readonly DiscordSocketClient _discordSocketClient;
        private readonly DiscordRestClient _discordRestClient;
        private readonly Config _config;

        public DiscordNotificationService(DiscordSocketClient discordSocketClient, DiscordRestClient discordRestClient, Config config)
        {
            _discordSocketClient = discordSocketClient;
            _discordRestClient = discordRestClient;
            _config = config;
        }

        public async Task<ulong> SendChannelMessage(ulong discordChannelId, Embed embed)
        {
            if (_discordSocketClient.GetChannel(discordChannelId) is ITextChannel channel)
            {
                IUserMessage result = null;

                try
                {
                    result = await channel.SendMessageAsync("", embed: embed);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Couldn't send message to {channel.Name} ({channel.Guild.Name}) - {e.Message} {e.InnerException}");
                    return 0;
                }

                return result.Id;
            }

            return 0;
        }

        public async Task<ulong> SendChannelMessage(ulong discordChannelId, string message, string title = null, Color? color = null)
        {
            var embed = new EmbedBuilder().WithDescription(message).WithColor(color ?? DiscordEmbedHelper.DEFAULT_EMBED_COLOUR);

            if (title != null) embed.WithTitle(title);

            return await SendChannelMessage(discordChannelId, embed.Build());
        }

        public async Task<ulong> SendChannelTextMessage(ulong discordChannelId, string message)
        {
            if (_discordSocketClient.GetChannel(discordChannelId) is ITextChannel channel)
            {
                var result = await channel.SendMessageAsync(message);

                return result.Id;
            }

            return 0;
        }

        public async Task<Dictionary<ulong, ulong>> SendChannelMessage(List<ulong> discordChannelIds, Embed embed, int batchDelaySeconds = 15)
        {
            var messageIds = new Dictionary<ulong, ulong>();

            int batchCount = 0;
            int batchLimit = 25;
            foreach (var discordChannelId in discordChannelIds)
            {
                try
                {
                    var discordMessageId = await SendChannelMessage(discordChannelId, embed);
                    if (discordMessageId != 0) messageIds.Add(discordChannelId, discordMessageId);
                }
                catch
                {
                    Console.WriteLine("Failed to send message to " + discordChannelId.ToString());
                }
                batchCount++;
                if (batchLimit == batchCount)
                {
                    batchCount = 0;
                    await Task.Delay(TimeSpan.FromSeconds(batchDelaySeconds));
                }
            }

            return messageIds;
        }

        public async Task<Dictionary<ulong, ulong>> SendChannelMessage(List<ulong> discordChannelIds, string message)
        {
            var messageIds = new Dictionary<ulong, ulong>();

            foreach (var discordChannelId in discordChannelIds)
            {
                var discordMessageId = await SendChannelMessage(discordChannelId, message);
                if (discordMessageId != 0) messageIds.Add(discordChannelId, discordMessageId);
            }

            return messageIds;
        }

        public async Task<ulong> SendUserMessage(ulong discordUserId, string message)
        {
            var user = await _discordRestClient.GetUserAsync(discordUserId);
            if (user != null && await user.GetOrCreateDMChannelAsync() is IDMChannel dmChannel)
            {
                var result = await dmChannel.SendMessageAsync(message);

                return result.Id;
            }

            return 0;
        }

        public async Task SendAuditChannelMessage(string message)
        {
            await SendChannelMessage(_config.DiscordConfig.AuditChannelId, message);
        }

        public async Task SendAuditChannelMessage(Embed embed)
        {
            await SendChannelMessage(_config.DiscordConfig.AuditChannelId, embed);
        }

        public async Task SendModChannelMessage(string message, string title)
        {
            await SendChannelMessage(_config.DiscordConfig.ModChannelId, message, title);
        }

        public async Task SendBanChannelMessage(string message)
        {
            var lastMessage = await GetBanChannelMessage();
            
            if (lastMessage != null)
            {
                await SendAuditChannelMessage("Deleted ban list\n" + lastMessage.Embeds?.FirstOrDefault()?.Description);

                var banChannel = await GetBanChannel();
                await banChannel.DeleteMessageAsync(lastMessage);
            }

            await SendChannelMessage(_config.DiscordConfig.BanListChannelId, new EmbedBuilder().WithTitle("Bans").WithDescription(message).Build());
        }

        public async Task<string> GetBanChannelMessageText()
        {
            var message = await GetBanChannelMessage();

            return message?.Embeds?.FirstOrDefault()?.Description;          
        }

        private async Task<IMessage> GetBanChannelMessage()
        {
            var banChannel = await GetBanChannel();
            var messages =  await banChannel.GetMessagesAsync(1, CacheMode.AllowDownload).FlattenAsync();

            return messages?.FirstOrDefault();
        }

        private async Task<ISocketMessageChannel> GetBanChannel()
        {
            return (_discordSocketClient.GetChannel(_config.DiscordConfig.BanListChannelId) as ISocketMessageChannel);
        }
    }
}