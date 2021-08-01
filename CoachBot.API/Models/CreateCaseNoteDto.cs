using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoachBot.Models
{
    public class CreateCaseNoteDto
    {
        public string Text { get; set; }

        public List<int> Images { get; set; }

    }
}
