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
    public class CaseNote : IUserUpdateableEntity
    {
        [Key]
        public int Id { get; set; }

        public string CaseNoteText { get; set; }

        public List<CaseNoteImage> CaseNoteImages { get; set; }

        public int CaseId { get; set; }

        public Case Case { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }

        public int? CreatedById { get; set; }

        public Player CreatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public int? UpdatedById { get; set; }

        public Player UpdatedBy { get; set; }

    }
}
