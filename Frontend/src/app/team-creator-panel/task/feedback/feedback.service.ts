import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError } from 'rxjs';
import { ErrorHandler } from 'src/app/error-handler';
import { Feedback } from './feedback';

@Injectable({
  providedIn: 'root'
})
export class FeedbackService {

  constructor(private httpClient: HttpClient) { }

  private baseUrl = window.location.origin + '/api/feedback';
  private headerOptions = new HttpHeaders({ 'Content-Type': 'application/json' });

  public getFeedback(teamMemberId: number) {
    const url = this.baseUrl + '/' + teamMemberId;

    return this.httpClient.get<Feedback[]>(
      url,
      { headers: this.headerOptions },
    ).pipe(catchError(ErrorHandler.handleError.bind(this)));
  }

  public getFeedbackByDateRange(teamMemberId: number, startDate: string, endDate: string) {
    const url = this.baseUrl + '/' + teamMemberId + '/start/' + startDate + '/end/' + endDate;

    return this.httpClient.get<Feedback[]>(
      url,
      { headers: this.headerOptions },
    ).pipe(catchError(ErrorHandler.handleError.bind(this)));
  }
}
