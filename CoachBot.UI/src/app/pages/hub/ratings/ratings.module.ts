import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SpinnerModule } from 'src/app/core/components/spinner/spinner.module';
import { RouterModule } from '@angular/router';
import { RatingsListComponent } from './ratings-list.component';
import { RatingsListRoutingModule } from './ratings.routing-module';
import { HubPipesModule } from '../shared/pipes/hub-pipes.module';

@NgModule({
    declarations: [
        RatingsListComponent
    ],
    imports: [
        CommonModule,
        RouterModule,
        FormsModule,
        SpinnerModule,
        RatingsListRoutingModule,
        HubPipesModule
    ]
})
export class RatingsListModule { }
