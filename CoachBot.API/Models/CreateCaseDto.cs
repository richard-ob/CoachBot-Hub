using CoachBot.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoachBot.Models
{
    public class CreateCaseDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public List<int> Images { get; set; }

        public CaseType CaseType { get; set; }

    }
}
