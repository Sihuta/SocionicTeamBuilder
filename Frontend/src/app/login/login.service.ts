import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable } from 'rxjs';
import { User } from '../admin-panel/user/user';
import { ErrorHandler } from '../error-handler';
import { Token } from './token';
import { LoginData } from './loginData';

@Injectable({
  providedIn: 'root'
})

export class LoginService {
  private baseUrl = window.location.origin;

  constructor(private httpClient: HttpClient) { }

  logIn(login: string, password: string): Observable<User> {
    const url = this.baseUrl + '/api/login';
    const body = JSON.stringify(login + " " + password);
    const headerOptions = new HttpHeaders({ 'Content-Type': 'application/json' });
    
    return this.httpClient.post<User>(
      url,
      body, {
        headers: headerOptions
    }).pipe(catchError(ErrorHandler.handleError.bind(this)));
  }

  getToken(loginData: LoginData): Observable<Token> {
    const url = this.baseUrl + '/api/token';
    const body = JSON.stringify(loginData);
    const headerOptions = new HttpHeaders({ 'Content-Type': 'application/json' });

    return this.httpClient.post<Token>(
      url,
      body, {
        headers: headerOptions
    }).pipe(catchError(ErrorHandler.handleError.bind(this)));
  }
}
