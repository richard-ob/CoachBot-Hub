import { Component, OnInit } from '@angular/core';
import { Ban } from '@pages/hub/shared/model/ban.model';
import { BanService } from '@pages/hub/shared/services/ban.service';

@Component({
    selector: 'app-ban-list',
    templateUrl: './ban-list.component.html'
})
export class BanListComponent implements OnInit {

    bans: Ban[];
    isUpdating = false;
    isRemoving = false;
    isLoading = true;

    constructor(private bansService: BanService) { }

    ngOnInit() {
        this.loadBans();
    }

    public loadBans() {        
        this.isLoading = true;
        this.bansService.getBans().subscribe((bans) => {
            this.bans = bans;
            this.isLoading = false;
        });
    }

}
