import { Player } from "@pages/hub/shared/model/player.model";
import { CaseNote } from "./case-note.model";
import { CaseType } from "./case-type.enum";

export interface Case {
    id?: number;
    caseTitle: string;
    caseStatus?: CaseStatus;
    caseType?: CaseType;
    caseManagerId?: number;
    caseManager: Player;
    caseNotes?: CaseNote[];
    createdBy?: Player;
    createdById?: number;
    closedDate?: Date;
    createdDate?: Date;    
}

export enum CaseStatus {
    Unassigned,
    PendingManagerResponse,
    PendingPlayerResponse,
    Closed
}
