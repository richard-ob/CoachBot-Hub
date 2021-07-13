using CoachBot.Database;
using CoachBot.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CoachBot.Domain.Model
{
    public class Ban : IUserUpdateableEntity
    {
        [Key]
        public int Id { get; set; }

        public BanType BanType { get; set; }

        public BanReason BanReason { get; }

        public string BanInfo { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int BannedPlayerId { get; set; }

        public Player BannedPlayer { get; set; }

        [JsonIgnore]
        public string BanDuration => throw new NotImplementedException();

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }

        public int? CreatedById { get; set; }

        public Player CreatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public int? UpdatedById { get; set; }

        public Player UpdatedBy { get; set; }
    }
}
