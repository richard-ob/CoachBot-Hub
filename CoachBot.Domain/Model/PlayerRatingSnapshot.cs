using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachBot.Domain.Model
{
    public class PlayerRatingSnapshot
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double? Rating { get; set; }

        public ulong? SteamID { get; set; }

        public TeamRole? TeamRole { get; set; }

        public string TeamName { get; set; }

    }
}
