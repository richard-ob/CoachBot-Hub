using CoachBot.Database;
using CoachBot.Domain.Services;
using CoachBot.Services;
using CoachBot.Shared.Helpers;
using Discord;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

internal class BanTimedHostedService : IHostedService, IDisposable
{
    private Timer _timer;
    private IServiceProvider _services;

    public BanTimedHostedService(IServiceProvider services)
    {
        _services = services;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(1));

        return Task.CompletedTask;
    }

    private async void DoWork(object state)
    {
        using (var scope = _services.CreateScope())
        {
            if (DateTime.UtcNow.Hour == 0)
            {
                var coachBotContext = scope.ServiceProvider.GetRequiredService<CoachBotContext>();
                var discordNotificationService = scope.ServiceProvider.GetRequiredService<DiscordNotificationService>();
                var discordClient = scope.ServiceProvider.GetRequiredService<DiscordSocketClient>();

                var bansToRemove = coachBotContext.Bans
                    .AsQueryable()
                    .Include(b => b.BannedPlayer)
                    .ToList()
                    .Where(b => b.EndDate.HasValue && b.EndDate.Value.Date.Ticks == DateTime.UtcNow.Date.Ticks && b.BannedPlayer.DiscordUserId.HasValue);

                foreach (var ban in bansToRemove)
                {
                    var officialGuild = discordClient.GetGuild(ConfigHelper.GetConfig().DiscordConfig.OwnerGuildId);

                    try
                    {
                        await officialGuild.RemoveBanAsync((ulong)ban.BannedPlayer.DiscordUserId);
                        await discordNotificationService.SendModChannelMessage($"{ban.BannedPlayer.Name}'s ban has expired", "Player Unbanned");
                    }
                    catch
                    {
                        await discordNotificationService.SendModChannelMessage($"{ban.BannedPlayer.Name}'s ban has expired but their Discord ban could not be removed", "Player Unbanned");
                    }
                }
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}