import { CaseType } from "../../case-type.enum";

export interface CreateCaseDto {
    title: string;
    description: string;
    caseType: CaseType;
    images: number[];
}
