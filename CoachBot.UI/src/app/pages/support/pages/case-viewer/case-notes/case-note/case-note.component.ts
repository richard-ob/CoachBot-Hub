import { Component, ChangeDetectionStrategy, OnInit, Input } from '@angular/core';
import { PlayerHubRole } from '@pages/hub/shared/model/player-hub-role.enum';
import { CaseNote } from '@pages/support/shared/case-note.model';
import { CaseService } from '@pages/support/shared/case.service';
import { Lightbox } from 'ngx-lightbox';

@Component({
    selector: 'app-case-note',
    templateUrl: './case-note.component.html',
    styleUrls: ['./case-note.component.scss']
})
export class CaseNoteComponent implements OnInit {

    @Input() caseNote: CaseNote;
    hubRoles = PlayerHubRole;
    isLoading = true;
    albums = [];

    constructor(private caseService: CaseService, private lightbox: Lightbox) { }

    ngOnInit(): void {
        for(const image of this.caseNote.caseNoteImages) {
            const src = image.assetImage.largeUrl;
            const caption = image.assetImage.fileName;
            const thumb = image.assetImage.smallUrl;
            const album = {
                src,
                caption,
                thumb
            };

            this.albums.push(album);
        }
    }

    open(index: number): void {
        this.lightbox.open(this.albums, index);
    }
    
    close(): void {
        this.lightbox.close();
    }

}

