using CoachBot.Bot.Preconditions;
using CoachBot.Domain.Model;
using CoachBot.Domain.Services;
using CoachBot.Preconditions;
using CoachBot.Services;
using CoachBot.Shared.Extensions;
using CoachBot.Tools;
using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace CoachBot.Modules
{
    [RequireChannelConfigured]
    [RequireChannelAndTeamActive]
    public class ModerationtModule : ModuleBase
    {
        private readonly ServerService _serverService;
        private readonly ChannelService _channelService;
        private readonly ServerManagementService _discordServerService;

        public ModerationtModule(ServerService serverService, ChannelService channelService, ServerManagementService discordServerService)
        {
            _serverService = serverService;
            _channelService = channelService;
            _discordServerService = discordServerService;
        }

        protected override void BeforeExecute(CommandInfo command)
        {
            base.BeforeExecute(command);
            CallContext.SetData(CallContextDataType.DiscordUser, Context.Message.Author.Username);
            Context.Message.AddReactionAsync(new Emoji("⚙️"));
        }

        protected override void AfterExecute(CommandInfo command)
        {
            base.AfterExecute(command);

            Context.Message.AddReactionAsync(new Emoji("✅"));
        }

        [Command("!updatebanlist ")]
        public async Task UpdateBanList(string banList)
        {

        }
    }
}