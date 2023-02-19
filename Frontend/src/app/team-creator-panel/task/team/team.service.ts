import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError } from 'rxjs';
import { ErrorHandler } from 'src/app/error-handler';
import { CreatedGroupOfTeams } from './createdGroupOfTeam';
import { Team } from './team';
import { TeamFullInfo } from './teamFullInfo';

@Injectable({
  providedIn: 'root'
})
export class TeamService {

  constructor(private httpClient: HttpClient) { }

  private baseUrl = window.location.origin + '/api/task/';
  private headerOptions = new HttpHeaders({ 'Content-Type': 'application/json' });

  public getTeams() {
    let url = this.baseUrl + localStorage.getItem('taskId') + "/teambuilder";
    return this.httpClient.get<CreatedGroupOfTeams[]>(url).pipe(catchError(ErrorHandler.handleError.bind(this)));
  }

  public getTeamsByBlacklist() {
    let url = this.baseUrl + localStorage.getItem('taskId') + "/teambuilder/byBlacklist";
    return this.httpClient.get<CreatedGroupOfTeams[]>(url).pipe(catchError(ErrorHandler.handleError.bind(this)));
  }

  public getCreatedTeams() {
    let url = this.baseUrl + localStorage.getItem('taskId') + "/teambuilder/teams";
    return this.httpClient.get<TeamFullInfo[]>(url).pipe(catchError(ErrorHandler.handleError.bind(this)));
  }

  public getTeamById(id: number) {
    let url = this.baseUrl + localStorage.getItem('taskId') + "/teambuilder" + "/" + id;
    return this.httpClient.get<Team>(url).pipe(catchError(ErrorHandler.handleError.bind(this)));
  }

  public createTeam(team: Team) {
    let url = this.baseUrl + localStorage.getItem('taskId') + "/teambuilder";
    const body = JSON.stringify(team);

    return this.httpClient.post<number>(
      url,
      body,
      { headers: this.headerOptions },
    ).pipe(catchError(ErrorHandler.handleError.bind(this)));
  }

  public deleteTeamsByTaskId() {
    let url = this.baseUrl + localStorage.getItem('taskId') + "/teambuilder";
    return this.httpClient.delete(url).pipe(catchError(ErrorHandler.handleError.bind(this)));
  }
}
