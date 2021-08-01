import { Component, ChangeDetectionStrategy, OnInit, Input } from '@angular/core';
import { PlayerHubRole } from '@pages/hub/shared/model/player-hub-role.enum';
import { Player } from '@pages/hub/shared/model/player.model';
import { PlayerService } from '@pages/hub/shared/services/player.service';
import { CaseNoteDto } from '@pages/support/shared/case-note-dto.interface';
import { CaseNote } from '@pages/support/shared/case-note.model';
import { CaseService } from '@pages/support/shared/case.service';
import { BatchAssetImageItem } from '@shared/components/asset-image-uploader/batch-asset-image-uploader/batch-asset-image-item.model';

@Component({
    selector: 'app-case-notes',
    templateUrl: './case-notes.component.html',
    styleUrls: ['./case-notes.component.scss']
})
export class CaseNotesComponent implements OnInit {

    @Input() caseId: number;
    @Input() showNoteCreator: boolean = true;
    caseNotes: CaseNote[];
    newCaseNote: CaseNoteDto;
    isLoading = true;
    isCreatingNote = false;
    isLoadingNotes = false;

    constructor(private caseService: CaseService, private playerService: PlayerService) {

    }

    ngOnInit(): void {
        this.newCaseNote = {
            text: ''
        };
        this.caseService.getCaseNotes(this.caseId).subscribe(caseNotes => {
            this.caseNotes = caseNotes;
        });
    }

    addCaseNote(): void {
        this.isCreatingNote = true;
        this.caseService.createCaseNote(this.caseId, this.newCaseNote).subscribe(() => {
            this.isCreatingNote = false;
            this.isLoadingNotes = true;
            this.caseService.getCaseNotes(this.caseId).subscribe((caseNotes) => {
                this.caseNotes = caseNotes;
                this.newCaseNote = {
                    text: ''
                };
                this.isLoadingNotes = false;
            });
        });
    }
    
    imageUploaded(files: BatchAssetImageItem[]) {
        this.newCaseNote.images = files.map(f => f.assetImageId);
    }

}
