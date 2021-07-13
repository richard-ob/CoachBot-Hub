import { Component, OnInit, Input } from '@angular/core';
import { MatchStatistics } from '@pages/hub/shared/model/match-statistics.model';
import { TournamentGroupMatch } from '@pages/hub/shared/model/tournament-group-match.model';
import { TournamentService } from '@pages/hub/shared/services/tournament.service';

@Component({
    selector: 'app-tournament-match-editor',
    templateUrl: './tournament-match-editor.component.html'
})
export class TournamentMatchEditorComponent implements OnInit {

    @Input() matchId: number;
    tournamentGroupMatch: TournamentGroupMatch;
    matchStatistics: MatchStatistics;
    isLoading = true;

    constructor(private tournamentService: TournamentService) { }

    ngOnInit() {
        this.loadMatch();
    }

    loadMatch() {
        this.isLoading = true;
        this.tournamentService.getTournamentGroupMatchForMatch(this.matchId).subscribe(match => {
            this.tournamentGroupMatch = match;
            this.isLoading = false;
        });
    }

    updateMatch() {
        this.isLoading = true;
        this.tournamentService.updateTournamentGroupMatch(this.tournamentGroupMatch).subscribe(() => {
            this.isLoading = false;
        });
    }

}
