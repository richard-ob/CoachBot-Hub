import { Component, OnInit } from '@angular/core';
import { BanService } from '../shared/services/ban.service';

@Component({
    selector: 'app-ban-manager',
    templateUrl: './ban-manager.component.html'
})
export class BanManagerComponent  {

    constructor(private bansService: BanService) { }

}
