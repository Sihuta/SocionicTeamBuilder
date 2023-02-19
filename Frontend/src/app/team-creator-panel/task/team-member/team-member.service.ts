import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError } from 'rxjs';
import { ErrorHandler } from 'src/app/error-handler';
import { TeamMember } from './teamMember';

@Injectable({
  providedIn: 'root'
})
export class TeamMemberService {

  constructor(private httpClient: HttpClient) { }

  private baseUrl = window.location.origin + '/api/teammember';
  private headerOptions = new HttpHeaders({ 'Content-Type': 'application/json' });

  public getByTeamAndEmployeeId(teamId: number, employeeId: number) {
    let url = this.baseUrl + '/team/' + teamId + '/employee/' + employeeId;
    return this.httpClient.get<TeamMember>(url).pipe(catchError(ErrorHandler.handleError.bind(this)));
  }
  
  public createTeamMember(teamMember: TeamMember) {
    const body = JSON.stringify(teamMember);

    return this.httpClient.post(
      this.baseUrl,
      body,
      { headers: this.headerOptions },
    ).pipe(catchError(ErrorHandler.handleError.bind(this)));
  }

  public updateTeamMember(teamMember: TeamMember) {
    const body = JSON.stringify(teamMember);

    return this.httpClient.put(
      this.baseUrl,
      body,
      { headers: this.headerOptions },
    ).pipe(catchError(ErrorHandler.handleError.bind(this)));
  }

  public getMembersByTaskId(taskId: string) {
    let url = this.baseUrl + "/" + taskId;
    return this.httpClient.get<TeamMember[]>(url).pipe(catchError(ErrorHandler.handleError.bind(this)));
  }
}
