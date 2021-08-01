using System.Text.RegularExpressions;

namespace CoachBot.Domain.Helpers
{
    public static class SteamIdHelper
    {
        public static ulong? ConvertSteamIDToSteamID64(string steamId)
        {
            var match = Regex.Match(steamId, @"^STEAM_[0-5]:[01]:\d+$", RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                return null;
            }

            var split = steamId.Split(":");

            var v = (ulong)76561197960265728;
            var y = ulong.Parse(split[1]);
            var z = ulong.Parse(split[2]);

            var w = (z * 2) + v + y;

            return w;
        }

        public static string ConvertSteamID64ToSteamID(ulong steamId64)
        {
            var steamId64long = MapUlongToLong(steamId64);
            var authserver = (steamId64long - 76561197960265728) & 1;
            var authid = (steamId64long - 76561197960265728 - authserver) / 2;

            return $"STEAM_0:{authserver}:{authid}";
        }

        private static long MapUlongToLong(ulong ulongValue)
        {
            return unchecked((long)ulongValue);
        }
    }
}