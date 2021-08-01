import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Ban, BanReason, BanType } from '@pages/hub/shared/model/ban.model';
import { Player } from '@pages/hub/shared/model/player.model';
import { BanService } from '@pages/hub/shared/services/ban.service';

@Component({
    selector: 'app-ban-editor-form',
    templateUrl: './ban-editor-form.component.html'
})
export class BanEditorFormComponent implements OnInit {

    @Output() banSaved = new EventEmitter<void>();
    @Input() ban: Ban;
    banTypes = BanType;
    banReasons = BanReason;
    selectedPlayer: Player;
    isPermanentBan = false;
    isSaving = false;

    constructor(private banService: BanService) {
        this.ban = { };
     }

    ngOnInit() {
        if (this.ban?.id && !this.ban.endDate) {
            this.isPermanentBan = true;
        }
    }

    setPlayer(player: Player) {
        this.selectedPlayer = player;
        this.ban.bannedPlayerId = player?.id;
    }

    togglePermanentBan() {
        this.isPermanentBan = !this.isPermanentBan;
        this.ban.endDate = null;
    }

    saveBan() {  
        this.isSaving = true;
        if (this.ban.id) {
            this.banService.updateBan(this.ban).subscribe(() => {
                this.banService.getBan(this.ban.id).subscribe((ban) => {
                    this.ban = ban;
                    this.isSaving = false;
                });
            });

        } else {          
            this.banService.createBan(this.ban).subscribe(() => {
                this.isPermanentBan = false;
                this.ban.endDate = null;
                this.selectedPlayer = null;
                this.ban = { };
                this.isSaving = false;
                this.banSaved.emit(null);
            });
        }
    }

}
