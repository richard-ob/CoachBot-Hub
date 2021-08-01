import { EventEmitter } from '@angular/core';
import { Component, OnInit, Output } from '@angular/core';
import { Ban } from '@pages/hub/shared/model/ban.model';
import { BanService } from '@pages/hub/shared/services/ban.service';

@Component({
    selector: 'app-ban-creator',
    templateUrl: './ban-creator.component.html'
})
export class BanCreatorComponent implements OnInit {

    @Output() banCreated = new EventEmitter<void>();
    newBan: Ban;

    constructor(private banService: BanService) {
        this.newBan = { };
     }

    ngOnInit() { }

    emitBanCreated() {
        this.banCreated.emit(null);
    }

}
