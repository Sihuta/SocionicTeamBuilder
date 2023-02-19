import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError } from 'rxjs';
import { ErrorHandler } from 'src/app/error-handler';
import { IotData } from './iot-data';

@Injectable({
  providedIn: 'root'
})
export class IotDataService {

  constructor(private httpClient: HttpClient) { }

  private baseUrl = window.location.origin + '/api/iotmeasurement';
  private headerOptions = new HttpHeaders({ 'Content-Type': 'application/json' });

  public getIotData(teamMemberId: number) {
    const url = this.baseUrl + '/' + teamMemberId;

    return this.httpClient.get<IotData[]>(
      url,
      { headers: this.headerOptions },
    ).pipe(catchError(ErrorHandler.handleError.bind(this)));
  }

  public getFixedIotData(teamMemberId: number) {
    const url = this.baseUrl + '/' + teamMemberId + '/fixedOne';

    return this.httpClient.get<IotData>(
      url,
      { headers: this.headerOptions },
    ).pipe(catchError(ErrorHandler.handleError.bind(this)));
  }

  public getIotDataByDateRange(teamMemberId: number, startDate: string, endDate: string) {
    const url = this.baseUrl + '/' + teamMemberId + '/start/' + startDate + '/end/' + endDate;

    return this.httpClient.get<IotData[]>(
      url,
      { headers: this.headerOptions },
    ).pipe(catchError(ErrorHandler.handleError.bind(this)));
  }
}
