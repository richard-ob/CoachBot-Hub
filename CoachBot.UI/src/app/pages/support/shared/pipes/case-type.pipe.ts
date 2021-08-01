import { Pipe, PipeTransform } from '@angular/core';
import { CaseType } from '../case-type.enum';

@Pipe({ name: 'caseType' })
export class CaseTypePipe implements PipeTransform {
    transform(caseType: CaseType): string {
        switch (caseType) {
            case CaseType.AppealBan:
                return $localize`:@@support.caseTypes.appealBan:Appeal Ban`;                
            case CaseType.Feedback:
                return $localize`:@@support.caseTypes.feedback:Feedback`;
            case CaseType.Issue:
                return $localize`:@@support.caseTypes.appeissuealBan:Issue`;
            case CaseType.Query:
                return $localize`:@@support.caseTypes.query:Query`;
            case CaseType.ReportPlayer:
                return $localize`:@@support.caseTypes.reportPlayer:Report Player`;
            default:
                return '';
        }
    }
}
