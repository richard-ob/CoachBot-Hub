import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { PlayerService } from '@pages/hub/shared/services/player.service';
import { PlayerStatisticFilters } from '@pages/hub/shared/model/dtos/paged-player-statistics-request-dto.model';
import { PlayerStatistics } from '@pages/hub/shared/model/player-statistics.model';
import { TimePeriod } from '@pages/hub/shared/model/time-period.enum';
import { UserPreferenceService, UserPreferenceType } from '@shared/services/user-preferences.service';
import { PlayerSpotlightStatistic } from './player-spotlight-statistic.enum';
import { Region } from '@pages/hub/shared/model/region.model';
import { RegionService } from '@pages/hub/shared/services/region.service';

@Component({
    selector: 'app-player-spotlight',
    templateUrl: './player-spotlight.component.html'
})
export class PlayerSpotlightComponent implements OnInit {

    @Input() statistic: PlayerSpotlightStatistic;
    @Input() tournamentId: number;
    filters = new PlayerStatisticFilters();
    spotlightPlayer: PlayerStatistics;
    apiModelProperty: string;
    modelProperty: string;
    measureName: string;
    positionFilter: string;
    usePercent: boolean = false;
    useDecimal: boolean = true;
    heading: string;
    ordering: string;
    iconClass: string;
    playerSpotlightStatistic = PlayerSpotlightStatistic;
    isLoading = true;

    constructor(
        private playerService: PlayerService,
        private regionService: RegionService,
        private userPreferencesService: UserPreferenceService,
        private router: Router
    ) {
    }

    ngOnInit() {
        if (this.tournamentId) {
            this.filters.tournamentId = this.tournamentId;
            this.useDecimal = false;
        } else {
            this.filters.timePeriod = TimePeriod.Week;
        }
        this.setProperties(this.statistic);
        this.filters.regionId = this.userPreferencesService.getUserPreference(UserPreferenceType.Region);
        this.filters.includeSubstituteAppearances = false;
        this.filters.minimumSecondsPlayed = 60 * 80; // Only include matches where player played at least roughly 80 mins
        if (this.filters.tournamentId) {
            this.filters.minimumAppearances = 5;
        } else {
            this.filters.minimumAppearances = 10;
        }
        this.regionService.getRegions().subscribe(regions => {
            const region = regions.find(r => r.regionId === this.filters.regionId);
            this.filters.matchFormat = region.matchFormat;
            this.playerService.getPlayerStatistics(1, 1, this.apiModelProperty, this.ordering, this.filters).subscribe(playerStatistics => {
                if (playerStatistics.items.length > 0) {
                    this.spotlightPlayer = playerStatistics.items[0];
                }
                this.isLoading = false;
            });
        });
    }

    setProperties(playerSpotlightStatistic: PlayerSpotlightStatistic) {
        switch (playerSpotlightStatistic) {
            case PlayerSpotlightStatistic.Goals:
                if (this.filters.tournamentId) {
                    this.modelProperty = 'goals';
                    this.apiModelProperty = 'Goals';
                    this.heading = $localize`:@@spotlight.tournament.goldenBoot:Golden Boot`;
                    this.measureName = $localize`:@@globals.goals:Goals`;
                } else {
                    this.modelProperty = 'goalsAverage';
                    this.apiModelProperty = 'GoalsAverage';
                    this.heading = $localize`:@@spotlight.goalScorersOfWeek:Goal Scorer of the Week`;
                    this.measureName = $localize`:@@globals.averageGoals:Average Goals`;
                }
                this.ordering = 'DESC';
                this.filters.positionName = null;
                this.iconClass = 'icon-soccer-ball';
                break;
            case PlayerSpotlightStatistic.Assists:
                if (this.filters.tournamentId) {
                    this.modelProperty = 'assists';
                    this.apiModelProperty = 'Assists';
                    this.heading = $localize`:@@spotlight.tournament.topAssister:Top Assister`;
                    this.measureName = $localize`:@@globals.assists:Assists`;
                } else {
                    this.modelProperty = 'assistsAverage';
                    this.apiModelProperty = 'AssistsAverage';
                    this.heading = $localize`:@@spotlight.assisterOfTheWeek:Assister of the Week`;
                    this.measureName = $localize`:@@globals.averageAssists:Average Assists`;                    
                }
                this.ordering = 'DESC';
                this.filters.positionName = null;
                this.iconClass = 'icon-soccer-shoe';
                break;
            case PlayerSpotlightStatistic.GoalsConceded:
                if (this.filters.tournamentId) {
                    this.modelProperty = 'keeperSavePercentage';
                    this.apiModelProperty = 'KeeperSavePercentage';
                    this.heading = $localize`:@@spotlight.tournament.bestKeeper:Best Keeper`;
                    this.measureName = $localize`:@@globals.saveRate:Save Rate`;
                    this.ordering = 'DESC';
                    this.usePercent = true;
                } else {
                    this.modelProperty = 'goalsConcededAverage';
                    this.apiModelProperty = 'GoalsConcededAverage';
                    this.heading = $localize`:@@spotlight.keeperOfTheWeek:Keeper of the Week`;
                    this.measureName = $localize`:@@globals.averageGoalsConceded:Average Goals Conceded`;
                    this.ordering = 'ASC';
                }
                this.filters.positionName = 'GK';
                this.iconClass = 'icon-keepers-glove';
                break;
            case PlayerSpotlightStatistic.PassCompletion:
                if (this.filters.tournamentId) {
                    this.modelProperty = 'passesCompleted';
                    this.apiModelProperty = 'PassesCompleted';
                    this.heading = $localize`:@@spotlight.tournament.topPasser:Top Passer`;
                    this.measureName = $localize`:@@globals.passesCompleted:Passes Completed`;
                } else {
                    this.modelProperty = 'passCompletionPercentageAverage';
                    this.apiModelProperty = 'PassCompletionPercentageAverage';
                    this.heading = $localize`:@@spotlight.passerOfTheWeek:Passer of the Week`;
                    this.measureName = $localize`:@@globals.passCompletion:Pass Completion`;
                    this.usePercent = true;
                }
                this.ordering = 'DESC';
                this.filters.positionName = null;
                this.iconClass = 'icon-soccer-shots';
                break;
        }
    }

    navigateToPlayerProfile() {
        this.router.navigate(['/player-profile', this.spotlightPlayer.playerId]);
    }

}
