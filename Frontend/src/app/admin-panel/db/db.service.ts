import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable } from 'rxjs';
import { ErrorHandler } from 'src/app/error-handler';

@Injectable({
  providedIn: 'root'
})

export class DbService {
  errorMessage: string = "";

  constructor(private httpClient: HttpClient) { }
  private baseUrl = window.location.origin + '/api/db';

  getBackupFiles(): Observable<string[]> {
    return this.httpClient.get<string[]>(this.baseUrl);
  }

  backup(): Observable<string> {
    return this.httpClient.get<string>(this.baseUrl + "/backup");
  }

  restore(backupFile: string): Observable<string> {
    return this.post(backupFile, "/restore");
  }

  post(strBody: string, specificUrl: string): Observable<string> {
    const url = this.baseUrl + specificUrl;
    const body = JSON.stringify(strBody);
    const headerOptions = new HttpHeaders({ 'Content-Type': 'application/json' });
    
    return this.httpClient.post<string>(
      url,
      body, {
        headers: headerOptions
    }).pipe(catchError(ErrorHandler.handleError.bind(this)));
  }
}
