import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TournamentService } from '@pages/hub/shared/services/tournament.service';
import { Tournament } from '@pages/hub/shared/model/tournament.model';
import { PlayerService } from '@pages/hub/shared/services/player.service';
import { Player } from '@pages/hub/shared/model/player.model';
import { TournamentStaffRole } from '@pages/hub/shared/model/tournament-staff-role.model';
import { TournamentGroupMatch } from '@pages/hub/shared/model/tournament-group-match.model';
import { TeamService } from '@pages/hub/shared/services/team.service';
import { ServerService } from '@pages/hub/shared/services/server.service';
import { UserPreferenceService, UserPreferenceType } from '@shared/services/user-preferences.service';
import { MapService } from '@pages/hub/shared/services/map.service';
import { Server } from '@pages/hub/shared/model/server.model';
import { Team } from '@pages/hub/shared/model/team.model';
import { Map } from '@pages/hub/shared/model/map.model';
import { Match } from '@pages/hub/shared/model/match.model';

@Component({
    selector: 'app-tournament-match-creator',
    templateUrl: './tournament-match-creator.component.html'
})
export class TournamentMatchCreatorComponent implements OnInit {

    tournamentId: number;
    tournamentPhaseId: number;
    tournamentGroupId: number;
    tournament: Tournament;
    tournamentGroupMatch: TournamentGroupMatch;
    servers: Server[];
    teams: Team[];
    maps: Map[];
    currentPlayer: Player;
    accessDenied: boolean;
    isLoading = true;

    constructor(
        private tournamentService: TournamentService,
        private route: ActivatedRoute,
        private playerService: PlayerService,
        private serverService: ServerService,
        private teamService: TeamService,
        private userPreferenceService: UserPreferenceService,
        private mapService: MapService,
        private router: Router
    ) { }

    ngOnInit() {
        this.route.paramMap.pipe().subscribe(params => {
            this.tournamentId = +params.get('id');
            this.tournamentGroupId = +params.get('tournamentGroupId');
            this.tournamentGroupMatch = {
                tournamentGroupId: this.tournamentGroupId,
                match: { } as Match
            } as TournamentGroupMatch;
            this.serverService.getServers().subscribe(servers => {
                this.servers = servers;
                this.teamService.getTeams(this.userPreferenceService.getUserPreference(UserPreferenceType.Region)).subscribe(teams => {
                    this.teams = teams;
                    this.mapService.getMaps().subscribe(maps => {
                        this.checkAccess();
                    });
                });
            });
        });
    }

    checkAccess() {
        this.isLoading = true;
        this.tournamentService.getTournament(this.tournamentId).subscribe(tournament => {
            this.playerService.getCurrentPlayer().subscribe((currentPlayer) => {
                if (tournament.tournamentStaff.some(staff => staff.playerId === currentPlayer.id && staff.role === TournamentStaffRole.Organiser)) {
                    this.tournament = tournament;
                } else {
                    this.accessDenied = true;
                }
                this.isLoading = false;
            });
        });
    }

    createMatch() {
        this.isLoading = true;
        this.tournamentService.createTournamentGroupMatch(this.tournamentGroupMatch, this.tournamentId).subscribe(() => {
            this.router.navigate(['/tournament-manager/' + this.tournamentId + '/groups']);
        });
    }

}
