import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Ban } from '@pages/hub/shared/model/ban.model';
import { BanService } from '@pages/hub/shared/services/ban.service';

@Component({
    selector: 'app-ban-editor',
    templateUrl: './ban-editor.component.html'
})
export class BanEditorComponent implements OnInit {

    banId: number;
    ban: Ban;
    isLoading = true;

    constructor(private route: ActivatedRoute, private banService: BanService) { }

    ngOnInit() {        
        this.route.paramMap.pipe().subscribe(params => {
            this.banId = +params.get('id');      
            this.loadBan();      
        });
    }

    loadBan() {
        this.banService.getBan(this.banId).subscribe((ban) => {
            this.ban = ban;
            this.isLoading = false;
        });
    }

}
