
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
import { Case } from './case.model';
import { CreateCaseDto } from './components/case-creator/create-case-dto.interface';
import { CaseNote } from './case-note.model';
import { CaseNoteDto } from './case-note-dto.interface';

@Injectable({
    providedIn: 'root'
})
export class CaseService {

    constructor(private http: HttpClient) { }
    
    createCase(caseToCreate: CreateCaseDto): Observable<number> {
        return this.http.post<number>(`${environment.apiUrl}/api/case`, caseToCreate);
    }

    updateCase(caseToUpdate: Case): Observable<void> {
        return this.http.put<void>(`${environment.apiUrl}/api/case/${caseToUpdate.id}`, caseToUpdate);
    }

    getCases(): Observable<Case[]> {
        return this.http.get<Case[]>(`${environment.apiUrl}/api/case/all`);
    }
    
    getCaseNotes(caseId: number): Observable<CaseNote[]> {
        return this.http.get<CaseNote[]>(`${environment.apiUrl}/api/case/${caseId}/notes`);
    }
    
    getCase(caseId: number): Observable<Case> {
        return this.http.get<Case>(`${environment.apiUrl}/api/case/${caseId}`);
    }

    createCaseNote(caseId: number, caseToCreate: CaseNoteDto): Observable<void> {
        return this.http.post<void>(`${environment.apiUrl}/api/case/${caseId}/notes`, caseToCreate);
    }

}
