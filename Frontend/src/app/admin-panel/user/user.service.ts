import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable } from 'rxjs';
import { ErrorHandler } from 'src/app/error-handler';
import { User } from './user';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private user: User = {
    id: 0,
    login: "",
    password: "",
    email: "",
    role: "",
    enterpriseId: 0
  };

  constructor(private httpClient: HttpClient) { }

  private baseUrl = window.location.origin + '/api/user';
  private headerOptions = new HttpHeaders({ 'Content-Type': 'application/json' });

  public getUsers() : Observable<User[]> {
    return this.httpClient.get<User[]>(this.baseUrl);
  }

  public deleteUser(id: number) {
    let url = this.baseUrl + "/" + id;

    return this.httpClient.delete(url)
      .pipe(catchError(ErrorHandler.handleError.bind(this)));
  }

  public updateUser(user: User) {
    const body = JSON.stringify(user);

    return this.httpClient.put(
      this.baseUrl,
      body,
      { headers: this.headerOptions }
    );
  }

  public createUser(User: User) {
    const body = JSON.stringify(User);

    return this.httpClient.post(
      this.baseUrl,
      body,
      { headers: this.headerOptions }
    ).pipe(catchError(ErrorHandler.handleError.bind(this)));
  }

  public getUser() : User {
    return this.user;
  }

  public setUser(user: User) {
    this.user = user;
  }
}
