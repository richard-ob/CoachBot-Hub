import { Player } from "@pages/hub/shared/model/player.model";
import { CaseNoteImage } from "./case-note-image.model";

export interface CaseNote {
    id?: number;
    caseNoteText: string;
    caseNoteImages: CaseNoteImage[];
    createdDate?: Date;
    createdBy?: Player;
    createdById?: number;
}
