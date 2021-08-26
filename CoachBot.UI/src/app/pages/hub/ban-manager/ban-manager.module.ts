import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SpinnerModule } from 'src/app/core/components/spinner/spinner.module';
import { RouterModule } from '@angular/router';
import { CoreModule } from '@core/core.module';
import { BanManagerRoutingModule } from './ban-manager.routing-module';
import { BanManagerComponent } from './ban-manager.component';
import { BanListComponent } from './ban-list/ban-list.component';
import { BanCreatorComponent } from './ban-creator/ban-creator.component';
import { BanEditorComponent } from './ban-editor/ban-editor.component';
import { HubPipesModule } from '../shared/pipes/hub-pipes.module';
import { PlayerSelectorModule } from '../shared/components/player-selector/player-selector.module';
import { MatDatepickerModule, MatNativeDateModule, MAT_DATE_LOCALE } from '@angular/material';
import { DlDateTimeDateModule, DlDateTimePickerModule } from 'angular-bootstrap-datetimepicker';
import { BanEditorFormComponent } from './ban-editor-form/ban-editor-form.component';
import { BanMessageManagerComponent } from './ban-message-manager/ban-message-manager.component';

@NgModule({
    declarations: [
        BanManagerComponent,
        BanListComponent,
        BanCreatorComponent,
        BanEditorComponent,
        BanEditorFormComponent,
        BanMessageManagerComponent
    ],
    imports: [
        CommonModule,
        CoreModule,
        RouterModule,
        FormsModule,
        SpinnerModule,
        HubPipesModule,
        BanManagerRoutingModule,
        PlayerSelectorModule, 
        MatDatepickerModule,
        MatNativeDateModule,
        DlDateTimeDateModule,
        DlDateTimePickerModule,
    ],
    providers: [
      { provide: MAT_DATE_LOCALE, useValue: 'en-GB' },
    ]
})
export class BanManagerModule { }
