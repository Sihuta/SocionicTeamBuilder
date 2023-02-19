import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable } from 'rxjs';
import { ErrorHandler } from 'src/app/error-handler';
import { Employee } from './employee';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private httpClient: HttpClient) { }

  private baseUrl = window.location.origin + '/api/employee';
  private headerOptions = new HttpHeaders({ 'Content-Type': 'application/json' });

  public getEmployees(enterprise: string) {
    const url = this.baseUrl + '/' + enterprise;

    return this.httpClient.get<Employee[]>(
      url,
      { headers: this.headerOptions },
    ).pipe(catchError(ErrorHandler.handleError.bind(this)));
  }

  public getEmployee(id: number) {
    const url = this.baseUrl + '/get/' + id;

    return this.httpClient.get<Employee>(
      url,
      { headers: this.headerOptions },
    ).pipe(catchError(ErrorHandler.handleError.bind(this)));
  }

  public registerEmployee(employee: Employee) {
    let url = this.baseUrl + "/new";
    const body = JSON.stringify(employee);
    
    return this.httpClient.post(
      url,
      body,
      { headers: this.headerOptions },
    ).pipe(catchError(ErrorHandler.handleError.bind(this)));
  }
}
