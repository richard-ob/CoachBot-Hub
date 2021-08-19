import { Component, OnInit } from '@angular/core';
import { Player } from '../shared/model/player.model';
import { PlayerService } from '../shared/services/player.service';

@Component({
    selector: 'ratings-team-list',
    templateUrl: './ratings-list.component.html',
    styleUrls: ['./ratings-list.component.scss']
})
export class RatingsListComponent implements OnInit {

    isLoading = true;
    ratedPlayers: Player[];
    rateablePlayers: Player[];

    constructor(private playerService: PlayerService) { }

    ngOnInit() {
     this.playerService.getRateablePlayers().subscribe(rateablePlayers => {
        this.rateablePlayers = rateablePlayers;
        this.playerService.getRatedPlayers().subscribe(ratedPlayers => {
            this.ratedPlayers = ratedPlayers;
            this.isLoading = false;
        });
     });
    }

   
}
