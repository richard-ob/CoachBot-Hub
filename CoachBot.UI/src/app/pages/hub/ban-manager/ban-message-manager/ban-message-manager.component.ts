import { Component, OnInit } from '@angular/core';
import { BanService } from '@pages/hub/shared/services/ban.service';

@Component({
    selector: 'app-ban-message-manager',
    templateUrl: './ban-message-manager.component.html'
})
export class BanMessageManagerComponent implements OnInit {

    banMessage: string;
    isLoading = true;

    constructor(private banService: BanService) {

     }

    ngOnInit() {
        this.loadBanMessage();
    }

    updateBanMessage(): void {
        this.isLoading = true;
        this.banService.updateBanMessage({ message: this.banMessage }).subscribe(() => {
            this.loadBanMessage();
        });
    }

    loadBanMessage(): void {
        this.isLoading = true;
        this.banService.getBanMessage().subscribe((banMessage) => {
            this.banMessage = banMessage.message;
            this.isLoading = false;
        });
    }

}
