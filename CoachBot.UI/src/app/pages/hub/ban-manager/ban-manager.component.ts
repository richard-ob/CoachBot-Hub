import { Component, OnInit } from '@angular/core';
import { BotService } from '../shared/services/bot.service';
import { PlayerService } from '../shared/services/player.service';
import { Player } from '../shared/model/player.model';
import { PlayerHubRole } from '../shared/model/player-hub-role.enum';
import { BanService } from '../shared/services/ban.service';
import { Ban } from '../shared/model/ban.model';

@Component({
    selector: 'app-ban-manager',
    templateUrl: './ban-manager.component.html'
})
export class BanManagerComponent implements OnInit {

    bans: Ban[];
    isUpdating = false;
    isRemoving = false;
    isLoading = true;

    constructor(private bansService: BanService) { }

    ngOnInit() {
        this.loadBans();
    }

    loadBans() {        
        this.isLoading = true;
        this.bansService.getBans().subscribe((bans) => {
            this.bans = bans;
            this.isLoading = false;
        });
    }

}
