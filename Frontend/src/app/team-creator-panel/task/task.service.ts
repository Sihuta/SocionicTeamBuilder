import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError } from 'rxjs';
import { ErrorHandler } from 'src/app/error-handler';
import { TeamMember } from './team-member/teamMember';
import { Task } from './task';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  constructor(private httpClient: HttpClient) { }

  private baseUrl = window.location.origin + '/api/task';
  private headerOptions = new HttpHeaders({ 'Content-Type': 'application/json' });

  public getTasks() {
    return this.httpClient.get<Task[]>(this.baseUrl).pipe(catchError(ErrorHandler.handleError.bind(this)));
  }

  public getTask() {
    let id = localStorage.getItem('taskId');
    let url = this.baseUrl + "/" + id;
    return this.httpClient.get<Task>(url).pipe(catchError(ErrorHandler.handleError.bind(this)));
  }

  public deleteTask(id: number) {
    let url = this.baseUrl + "/" + id;

    return this.httpClient.delete(url)
      .pipe(catchError(ErrorHandler.handleError.bind(this)));
  }

  public updateTask(task: Task) {
    const body = JSON.stringify(task);

    return this.httpClient.put(
      this.baseUrl,
      body,
      { headers: this.headerOptions }
    );
  }

  public createTask(task: Task) {
    const body = JSON.stringify(task);

    return this.httpClient.post<number>(
      this.baseUrl,
      body,
      { headers: this.headerOptions },
    ).pipe(catchError(ErrorHandler.handleError.bind(this)));
  }
}
