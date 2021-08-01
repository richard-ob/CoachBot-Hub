import { Component, OnInit } from '@angular/core';
import { Case } from '../../case.model';
import { CaseService } from '../../case.service';

@Component({
    selector: 'app-case-list',
    templateUrl: './case-list.component.html',
    styleUrls: ['./case-list.component.scss']
})
export class CaseListComponent implements OnInit {

    isLoading = true;
    cases: Case[];

    constructor(private caseService: CaseService) { }

    ngOnInit(): void {
        this.isLoading = false;
        this.caseService.getCases().subscribe((cases) => {
            this.cases = cases;
        });
    }

}
