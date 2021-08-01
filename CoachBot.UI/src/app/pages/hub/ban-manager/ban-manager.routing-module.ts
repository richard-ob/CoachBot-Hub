import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BanEditorComponent } from './ban-editor/ban-editor.component';
import { BanManagerComponent } from './ban-manager.component';

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
    }
];
@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
    providers: []
})
export class BanManagerRoutingModule { }
