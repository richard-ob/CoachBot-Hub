import { Component, ChangeDetectionStrategy, OnInit } from '@angular/core';

@Component({
    selector: 'app-case-note-editor',
    templateUrl: './case-note-editor.component.html',
    styleUrls: ['./case-note-editor.component.scss']
})
export class CaseNoteEditorComponent implements OnInit {

    isLoading = true;

    constructor() { }

    ngOnInit(): void {
        this.isLoading = false;
    }

}
