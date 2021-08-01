import { Component, ChangeDetectionStrategy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PlayerHubRole } from '@pages/hub/shared/model/player-hub-role.enum';
import { Player } from '@pages/hub/shared/model/player.model';
import { PlayerService } from '@pages/hub/shared/services/player.service';
import { Case, CaseStatus } from '@pages/support/shared/case.model';
import { CaseService } from '@pages/support/shared/case.service';

@Component({
    selector: 'app-case-viewer',
    templateUrl: './case-viewer.component.html',
    styleUrls: ['./case-viewer.component.scss']
})
export class CaseViewerComponent implements OnInit {

    showManagementOptions = false;
    currentPlayer: Player;
    caseId: number;
    case: Case;
    caseStatuses = CaseStatus;
    isLoading = true;
    isUpdatingCase = false;

    constructor(private caseService: CaseService, private route: ActivatedRoute, private playerService: PlayerService) {
        this.playerService.getCurrentPlayer().subscribe((player) => {
            this.currentPlayer = player;
            if (this.currentPlayer.hubRole >= PlayerHubRole.Manager) {
                this.showManagementOptions = true;
            }
        });
    }

    ngOnInit(): void {
        this.isLoading = false;
        this.route.paramMap.pipe().subscribe(params => {
            this.caseId = +params.get('id');      
            this.loadCase();      
        });
    }

    loadCase(): void {
        this.caseService.getCase(this.caseId).subscribe((existingCase) => {
            this.case = existingCase;
            this.isUpdatingCase = false;
        });
    }

    closeTicket(): void {
        this.case.caseStatus = CaseStatus.Closed;
        this.updateCase();  
    }

    reopenTicket(): void {
        this.case.caseStatus = CaseStatus.PendingManagerResponse;
        this.case.closedDate = null;
        this.updateCase();  
    }

    claimTicket(): void {
        this.case.caseManagerId = this.currentPlayer.id;
        this.updateCase();     
    }

    updateCase(): void {
        this.isUpdatingCase = true;
        this.caseService.updateCase(this.case).subscribe(() => { 
            this.loadCase();  
        });
    }

}
