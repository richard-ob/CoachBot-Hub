import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Team } from '@pages/hub/shared/model/team.model';
import { TeamService } from '@pages/hub/shared/services/team.service';
import { UserPreferenceService, UserPreferenceType } from '@shared/services/user-preferences.service';

@Component({
    selector: 'app-team-head-to-head-selector',
    templateUrl: './team-head-to-head-selector.component.html',
    styleUrls: ['../team-head-to-head.component.scss']
})
export class TeamHeadToHeadSelectorComponent implements OnInit {
  
    isLoading = true;
    includeInactive = false;
    teams: Team[];
    teamOneCode: string;
    teamTwoCode: string;

    constructor(private teamService: TeamService, private router: Router, private userPreferenceService: UserPreferenceService) { }

    ngOnInit(): void {
        this.loadTeams();
    }
    
    loadTeams(): void {
        this.isLoading = true;
        const regionId = this.userPreferenceService.getUserPreference(UserPreferenceType.Region);
        this.teamService.getTeams(regionId, undefined).subscribe((teams) => {
            this.teams = teams.sort((a, b) => a.name.localeCompare(b.name)).filter(t => !t.inactive || this.includeInactive);
            this.isLoading = false;
        });
    }

    setTeamOneCode(teamCode: string) {
        this.teamOneCode = teamCode.toLowerCase();
    }

    setTeamTwoCode(teamCode: string) {
        this.teamTwoCode = teamCode.toLowerCase();
    }

    navigateIfBothTeamsSelected() {
        if (this.teamOneCode && this.teamTwoCode && (this.teamOneCode != this.teamTwoCode)) {
            this.router.navigate(['team-head-to-head', this.teamOneCode, this.teamTwoCode]);
            this.isLoading = true;
        }
    }

}
