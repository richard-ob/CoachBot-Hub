using CoachBot.Database;
using CoachBot.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachBot.Domain.Services
{
    public class CaseService
    {
        private readonly CoachBotContext _dbContext;

        public CaseService(CoachBotContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Case
        public int CreateCase(Case casetoCreate)
        {
            var newCase = new Case()
            {
                CaseTitle = casetoCreate.CaseTitle,
                CaseStatus = CaseStatus.Unassigned,
                CaseType = casetoCreate.CaseType
            };

            _dbContext.Cases.Add(newCase);

            _dbContext.SaveChanges();

            return _dbContext.Entry(newCase).Property(m => m.Id).CurrentValue;            
        }

        public void UpdateCase(Case caseToUpdate)
        {
            var existingCase = _dbContext.Cases.Single(c => c.Id == caseToUpdate.Id);

            existingCase.CaseManagerId = caseToUpdate.CaseManagerId;
            existingCase.CaseStatus = caseToUpdate.CaseStatus;
            existingCase.CaseType = caseToUpdate.CaseType;
            existingCase.CaseTitle = caseToUpdate.CaseTitle;

            _dbContext.SaveChanges();
        }

        public Case GetCase(int caseId)
        {
            return _dbContext.Cases.Single(c => c.Id == caseId);
        }

        public List<Case> GetAllCases()
        {
            return _dbContext.Cases.ToList();
        }

        public List<Case> GetCasesForUser(int playerId)
        {
            return _dbContext.Cases.AsQueryable().Where(c => c.CaseManagerId == playerId || c.CreatedById == playerId).ToList();
        }

        public List<Case> GetUnassignedCases()
        {
            return _dbContext.Cases.AsQueryable().Where(c => c.CaseManagerId == null).ToList();
        }
        #endregion

        #region Case Notes
        public void CreateCaseNote(CaseNote caseNote)
        {
            var newCaseNote = new CaseNote()
            {
                CaseId = caseNote.CaseId,
                CaseNoteText = caseNote.CaseNoteText,
                CaseNoteImages = caseNote.CaseNoteImages
            };

            _dbContext.CaseNotes.Add(caseNote);
            _dbContext.SaveChanges();
        }

        public List<CaseNote> GetNotesForCase(int caseId)
        {
            return _dbContext.CaseNotes
                .AsQueryable()
                .Where(c => c.CaseId == caseId)
                .Include(c => c.CaseNoteImages)
                    .ThenInclude(c => c.AssetImage)
                .ToList();
        }
        #endregion
    }
}
