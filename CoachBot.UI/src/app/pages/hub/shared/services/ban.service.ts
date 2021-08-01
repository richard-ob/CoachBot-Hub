import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Ban } from '../model/ban.model';

@Injectable({
    providedIn: 'root'
})
export class BanService {

    constructor(private http: HttpClient) { }

    getBans(): Observable<Ban[]> {
        return this.http.get<Ban[]>(`${environment.apiUrl}/api/ban`).pipe();
    }

    getBan(banId: number): Observable<Ban> {
        return this.http.get<Ban>(`${environment.apiUrl}/api/ban/${banId}`).pipe();
    }

    createBan(ban: Ban) {
        return this.http.post(`${environment.apiUrl}/api/ban`, ban).pipe();
    }

    updateBan(ban: Ban) {
        return this.http.put(`${environment.apiUrl}/api/ban/${ban.id}`, ban).pipe();
    }

    deleteBan(ban: Ban) {
        return this.http.delete(`${environment.apiUrl}/api/ban/${ban.id}`, null).pipe();
    }
}
