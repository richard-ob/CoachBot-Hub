import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RatingsListComponent } from './ratings-list.component';

const routes: Routes = [
    {
        path: 'ratings-list',
        component: RatingsListComponent,
        data: { title: $localize`:@@globals.ratingsList:Ratings List` }
    },
];
@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
    providers: []
})
export class RatingsListRoutingModule { }
