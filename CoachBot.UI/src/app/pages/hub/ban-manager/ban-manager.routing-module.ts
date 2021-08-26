import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BanEditorComponent } from './ban-editor/ban-editor.component';
import { BanManagerComponent } from './ban-manager.component';
import { BanMessageManagerComponent } from './ban-message-manager/ban-message-manager.component';

const routes: Routes = [
    {
        path: 'bans',
        component: BanManagerComponent,
        data: { title: 'Ban Manager' }    
    },
    {
        path: 'ban/:id',
        component: BanEditorComponent,
        data: { title: 'Ban Editor' }
    },
    {
        path: 'ban-message-manager',
        component: BanMessageManagerComponent,
        data: { title: 'Ban Message Manager' }
    }
];
@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
    providers: []
})
export class BanManagerRoutingModule { }
