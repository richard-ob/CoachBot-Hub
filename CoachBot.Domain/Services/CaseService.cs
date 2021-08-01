using CoachBot.Database;
using CoachBot.Domain.Model;
using CoachBot.Shared.Extensions;
using CoachBot.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoachBot.Domain.Services
{
    public class CaseService
    {
        private readonly CoachBotContext _dbContext;
        private readonly DiscordNotificationService _discordNotificationService;

        public CaseService(CoachBotContext dbContext, DiscordNotificationService discordNotificationService)
        {
            _dbContext = dbContext;
            _discordNotificationService = discordNotificationService;
        }

        #region Case
        public int CreateCase(string title, string description, CaseType caseType, List<int> images)
        {
            var caseNoteImages = new List<CaseNoteImage>();
            foreach (var image in images)
            {
                caseNoteImages.Add(new CaseNoteImage() { AssetImageId = image });
            }

            var newCase = new Case()
            {
                CaseTitle = title,
                CaseStatus = CaseStatus.Unassigned,
                CaseType = caseType,
                CaseNotes = new List<CaseNote>()
                {
                    new CaseNote()
                    {
                        CaseNoteText = description,
                        CaseNoteImages = caseNoteImages
                    }
                }
            };

            _dbContext.Cases.Add(newCase);

            _dbContext.SaveChanges();

            var caseId = _dbContext.Entry(newCase).Property(m => m.Id).CurrentValue;
            var playerId = CallContext.GetData(CallContextDataType.PlayerId);
            var player = _dbContext.Players.Find(playerId);
            _discordNotificationService.SendModChannelMessage($"`{newCase.CaseTitle}` by {player.Name} - http://www.iosoccer.com/support/{caseId}", "New Ticket Raised").Wait();

            return caseId;         
        }

        public void UpdateCase(Case caseToUpdate)
        {
            var existingCase = _dbContext.Cases.Single(c => c.Id == caseToUpdate.Id);

            existingCase.CaseManagerId = caseToUpdate.CaseManagerId;
            existingCase.CaseStatus = caseToUpdate.CaseStatus;
            existingCase.CaseType = caseToUpdate.CaseType;
            existingCase.CaseTitle = caseToUpdate.CaseTitle;

            if (caseToUpdate.CaseStatus == CaseStatus.Closed && existingCase.ClosedDate == null)
            {
                existingCase.ClosedDate = DateTime.UtcNow;
            }
            else if (caseToUpdate.CaseStatus != CaseStatus.Closed && existingCase.ClosedDate != null)
            {
                existingCase.ClosedDate = null;
            }

            _dbContext.SaveChanges();

        }

        public Case GetCase(int caseId)
        {
            return _dbContext.Cases
                .Include(c => c.CaseManager)
                .Include(c => c.CreatedBy)
                .Single(c => c.Id == caseId);
        }

        public List<Case> GetAllCases()
        {
            var playerId = CallContext.GetData(CallContextDataType.PlayerId);
            var player = _dbContext.Players.Find(playerId);

            if (player.HubRole == PlayerHubRole.Player)
            {
                return _dbContext.Cases.AsQueryable().Where(c => c.CreatedById == (int)playerId).OrderByDescending(c => c.CreatedDate).ToList();
            }

            return _dbContext.Cases.AsQueryable().OrderByDescending(c => c.CreatedDate).ToList();
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
        public void CreateCaseNote(int caseId, string caseNoteText, List<int> images)
        {
            var caseNoteImages = new List<CaseNoteImage>();
            if (images != null)
            {
                foreach (var image in images)
                {
                    caseNoteImages.Add(new CaseNoteImage() { AssetImageId = image });
                }

            }
            var newCaseNote = new CaseNote()
            {
                CaseId = caseId,
                CaseNoteText = caseNoteText,
                CaseNoteImages = caseNoteImages
            };

            _dbContext.CaseNotes.Add(newCaseNote);
            _dbContext.SaveChanges();

            var playerId = CallContext.GetData(CallContextDataType.PlayerId);
            var player = _dbContext.Players.Find(playerId);
            if (player.HubRole == PlayerHubRole.Owner)
            {
                var caseTitle = _dbContext.Cases.Find(caseId).CaseTitle;
                _discordNotificationService.SendModChannelMessage($"`{caseTitle}` by {player.Name} - http://www.iosoccer.com/support/{caseId} {Environment.NewLine} ```{HtmlHelper.StripHTML(caseNoteText).Truncate(2000)}```", "Ticket Updated").Wait();
            }
        }

        public List<CaseNote> GetNotesForCase(int caseId)
        {
            return _dbContext.CaseNotes
                .AsQueryable()
                .Where(c => c.CaseId == caseId)
                .Include(c => c.CaseNoteImages)
                    .ThenInclude(c => c.AssetImage)
                .Include(c => c.CreatedBy)
                .OrderByDescending(c => c.CreatedDate)
                .ToList();
        }
        #endregion
    }
}
