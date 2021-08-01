import { Pipe, PipeTransform } from '@angular/core';
import { CaseStatus } from '../case.model';

@Pipe({ name: 'caseStatus' })
export class CaseStatusPipe implements PipeTransform {
    transform(caseStatus: CaseStatus): string {
        switch (caseStatus) {
            case CaseStatus.Closed:
                return $localize`:@@support.caseStatus.closed:Closed`;                
            case CaseStatus.PendingManagerResponse:
                return $localize`:@@support.caseStatus.pendingSupportResponse:Pending Support Response`;                        
            case CaseStatus.PendingPlayerResponse:
                return $localize`:@@support.caseStatus.pendingPlayerResponse:Pending Player Response`;                                
            case CaseStatus.Unassigned:
                return $localize`:@@support.caseStatus.pendingResponse:Awaiting Assignment`;
            default:
                return '';
        }
    }
}
