import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CaseViewerComponent } from './pages/case-viewer/case-viewer.component';
import { SupportComponent } from './support.component';

const routes: Routes = [
    {
        path: 'support',
        component: SupportComponent,
        data: { title: $localize`:@@globals.support:Support` }
    },    
    {
        path: 'support/:id',
        component: CaseViewerComponent,
        data: { title: $localize`:@@globals.supportTicket:Support Ticket` }
    }
];
@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
    providers: []
})
export class SupportRoutingModule { }
