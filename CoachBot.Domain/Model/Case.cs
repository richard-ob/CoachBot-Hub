using CoachBot.Database;
using CoachBot.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachBot.Domain.Model
{
    public class Case : IUserUpdateableEntity
    {
        [Key]
        public int Id { get; set; }

        public CaseType CaseType { get; set; }

        public CaseStatus CaseStatus { get; set; }

        public string CaseTitle { get; set; }

        public int? CaseManagerId { get; set; }

        public Player CaseManager { get; set; }

        public ICollection<CaseNote> CaseNotes { get; set; }

        public DateTime? ClosedDate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }

        public int? CreatedById { get; set; }

        public Player CreatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public int? UpdatedById { get; set; }

        public Player UpdatedBy { get; set; }

    }
}
