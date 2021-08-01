import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CaseViewerComponent } from './pages/case-viewer/case-viewer.component';
import { CaseNotesComponent } from './pages/case-viewer/case-notes/case-notes.component';
import { SupportComponent } from './support.component';
import { SupportRoutingModule } from './support.routing-module';
import { CaseNoteEditorComponent } from './shared/components/case-note-editor/case-note-editor.component';
import { CaseCreatorComponent } from './shared/components/case-creator/case-creator.component';
import { CaseListComponent } from './shared/components/case-list/case-list.component';
import { SpinnerModule } from '@core/components/spinner/spinner.module';
import { CaseTypePipe } from './shared/pipes/case-type.pipe';
import { CaseStatusPipe } from './shared/pipes/case-status.pipe';
import { CaseNoteComponent } from './pages/case-viewer/case-notes/case-note/case-note.component';
import { QuillModule } from 'ngx-quill';
import { CaseStatusBadgeComponent } from './shared/components/case-status-badge/case-status-badge.component';
import { RecaptchaFormsModule, RecaptchaModule } from 'ng-recaptcha';
import { AssetImageUploaderModule } from '@shared/components/asset-image-uploader/asset-image-uploader.module';

    const quillConfig = {
        modules: {
            toolbar: [
                ['bold', 'italic', 'underline', 'strike'],        // toggled buttons
                ['blockquote', 'code-block'],
            
                [{ 'header': 1 }, { 'header': 2 }],               // custom button values
                [{ 'list': 'ordered'}, { 'list': 'bullet' }],
                [{ 'script': 'sub'}, { 'script': 'super' }],      // superscript/subscript
                [{ 'indent': '-1'}, { 'indent': '+1' }],          // outdent/indent
                [{ 'direction': 'rtl' }],                         // text direction
            
                [{ 'size': ['small', false, 'large', 'huge'] }],  // custom dropdown
                [{ 'header': [1, 2, 3, 4, 5, 6, false] }],
            
                [{ 'color': [] }, { 'background': [] }],          // dropdown with defaults from theme
                [{ 'font': [] }],
                [{ 'align': [] }],
            
                ['clean'],                                         // remove formatting button
            
            ]
        }
    };

@NgModule({
    declarations: [
        CaseListComponent,
        CaseNoteEditorComponent,
        CaseCreatorComponent,
        CaseViewerComponent,
        CaseNotesComponent,
        CaseNoteComponent,
        CaseStatusBadgeComponent,
        SupportComponent,
        CaseStatusPipe,
        CaseTypePipe
    ],
    imports: [
        CommonModule,
        FormsModule,
        SupportRoutingModule,
        SpinnerModule,
        QuillModule.forRoot(quillConfig),        
        RecaptchaModule,
        RecaptchaFormsModule,
        AssetImageUploaderModule
    ]
})
export class SupportModule { }
