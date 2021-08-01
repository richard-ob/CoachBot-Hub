import { Component, Input } from '@angular/core';
import { CaseStatus } from '../../case.model';

@Component({
    selector: 'app-case-status-badge',
    templateUrl: './case-status-badge.component.html',
    styleUrls: ['./case-status-badge.component.scss']
})
export class CaseStatusBadgeComponent {

    @Input() caseStatus: CaseStatus;
    caseStatuses = CaseStatus;

    constructor() { }

}
