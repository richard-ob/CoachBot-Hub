using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachBot.Domain.Model
{
    public class CaseNoteImage
    {
        public int CaseNoteId { get; set; }

        public CaseNote CaseNote { get; set; }

        public int AssetImageId { get; set; }

        public AssetImage AssetImage { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }
    }
}
