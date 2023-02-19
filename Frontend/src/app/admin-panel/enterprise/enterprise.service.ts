import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { ErrorHandler } from 'src/app/error-handler';
import { Enterprise } from './enterprise';

@Injectable({
  providedIn: 'root'
})

export class EnterpriseService {
  private enterprise: Enterprise = {
    id: 0,
    name: "",
    location: "",
    activity: ""
  };
  errorMessage: string = "";

  constructor(private httpClient: HttpClient) { }

  private baseUrl = window.location.origin + '/api/enterprise';
  private headerOptions = new HttpHeaders({ 'Content-Type': 'application/json' });

  public getEnterprises() : Observable<Enterprise[]> {
    return this.httpClient.get<Enterprise[]>(this.baseUrl);
  }

  public getEnterpriseByUserId(userId: string): Observable<string> {
    let url = this.baseUrl + "/" + userId;
    return this.httpClient.get<string>(url);
  }

  public deleteEnterprise(id: number) {
    let url = this.baseUrl + "/" + id;

    return this.httpClient.delete(url)
      .pipe(catchError(ErrorHandler.handleError.bind(this)));
  }

  public updateEnterprise(enterprise: Enterprise) {
    const body = JSON.stringify(enterprise);

    return this.httpClient.put(
      this.baseUrl,
      body,
      { headers: this.headerOptions }
    );
  }

  public createEnterprise(enterprise: Enterprise) {
    const body = JSON.stringify(enterprise);

    return this.httpClient.post(
      this.baseUrl,
      body,
      { headers: this.headerOptions }
    ).pipe(catchError(ErrorHandler.handleError.bind(this)));
  }

  public getEnterprise() : Enterprise {
    return this.enterprise;
  }

  public setEnterprise(enterprise: Enterprise) {
    this.enterprise = enterprise;
  }
}
