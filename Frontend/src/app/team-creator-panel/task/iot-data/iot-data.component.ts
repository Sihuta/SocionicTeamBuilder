import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { EmployeeService } from '../../employee/employee.service';
import { TeamMemberService } from '../team-member/team-member.service';
import { TeamMember } from '../team-member/teamMember';
import { IotData } from './iot-data';
import { IotDataService } from './iot-data.service';

@Component({
  selector: 'app-iot-data',
  templateUrl: './iot-data.component.html',
  styleUrls: ['./iot-data.component.css']
})
export class IotDataComponent implements OnInit {

  teamId: number;
  employeeId: number;

  employeeName: string = "";
  teamMember: TeamMember = new TeamMember(0, 0, 0, 0, "");

  iotData: IotData[] = [];
  forOneDay: boolean = true;

  minDate: Date = new Date("5/27/22");
  maxDate: Date = new Date("6/30/22");

  startDate: Date = new Date();
  endDate: Date = new Date();

  constructor(
    private readonly employeeService: EmployeeService,
    private readonly teamMemberService: TeamMemberService,
    private readonly iotDataService: IotDataService,
    private readonly translate: TranslateService,

    activateRoute: ActivatedRoute) {
      this.teamId = activateRoute.snapshot.params['id'];
      this.employeeId = activateRoute.snapshot.params['employeeId'];
  }

  ngOnInit(): void {
    this.loadData();
  }

  startDateChanged($event: any) {
    this.startDate = $event.value;
  }

  endDateChanged($event: any) {
    this.endDate = $event.value;
    if (this.startDate && this.endDate) {
      this.loadIotDataByDateRange(this.startDate.toDateString(), this.endDate.toDateString());
    }
  }

  loadData() {
    this.loadName();
    this.loadIotData();
  }

  loadName() {
    this.employeeService.getEmployee(this.employeeId)
    .subscribe(res => this.employeeName = res.fullName);
  }

  loadIotData() {
    this.teamMemberService.getByTeamAndEmployeeId(this.teamId, this.employeeId)
    .subscribe(res => {
      this.teamMember = res;

      this.iotDataService.getIotData(this.teamMember.id)
      .subscribe(res => {
        this.iotData = res;
        // this.forOneDay = res[0].dateTime.getDate() == res[res.length - 1].dateTime.getDate();

        this.loadDataSets(res);

        // if (res.length > 0) {
        //   this.maxDate = res[0].dateTime;
        //   this.minDate = res[res.length - 1].dateTime;
        // }

        // this.startDate = this.minDate;
        // this.endDate = this.maxDate;
      });
    });
  }

  loadIotDataByDateRange(startDate: string, endDate: string) {
    this.iotDataService.getIotDataByDateRange(this.teamMember.id, startDate, endDate)
    .subscribe(res => {
      this.iotData = res;
      // this.forOneDay = res[0].dateTime.getDate() == res[res.length - 1].dateTime.getDate();
      this.loadDataSets(res);
    });
  }

  loadDataSets(iotData: IotData[]) {
    for (let i = 0; i < iotData.length; ++i) {
      this.heartBeatSet[i] = iotData[i].heartBeat;
      this.bodyTemperatureSet[i] = iotData[i].bodyTemperature;
      this.pulseSet[i] = iotData[i].pulse;

      if (this.forOneDay) {
        let time = new DatePipe('en-US').transform(iotData[i].dateTime, 'HH:mm:ss');
        if (time) {
          this.dateSet.push(time);
        }
      }

      // if (this.forOneDay) {
      //   let time = new DatePipe('en-US').transform(element.dateTime, 'dd/MM/yy HH:mm:ss');
      //   if (time) {
      //     this.dateSet.push(time);
      //   }
      // } else {
      //   let dateTime = new DatePipe('en-US').transform(element.dateTime, 'dd/MM/yy HH:mm:ss');
      //   if (dateTime) {
      //     this.dateSet.push(dateTime);
      //   }
      // }
    }

    this.loadMinMaxValues(iotData.length);

    let minLbl = "";
    let maxLbl = "";
    let heartBeatLbl = "";
    let temperatureLbl = "";
    let pulseLbl = "";

    this.translate.stream("TEAM-CREATOR_PANEL.TEAM.IOT_DATA.MIN_LBL").subscribe(res => minLbl = res);
    this.translate.stream("TEAM-CREATOR_PANEL.TEAM.IOT_DATA.MAX_LBL").subscribe(res => maxLbl = res);
    this.translate.stream("TEAM-CREATOR_PANEL.TEAM.IOT_DATA.HEART_BEAT_LBL").subscribe(res => heartBeatLbl = res);
    this.translate.stream("TEAM-CREATOR_PANEL.TEAM.IOT_DATA.TEMPERATURE_LBL").subscribe(res => temperatureLbl = res);
    this.translate.stream("TEAM-CREATOR_PANEL.TEAM.IOT_DATA.PULSE_LBL").subscribe(res => pulseLbl = res);


    this.chart1Datasets = [
      { data: this.minHeartBeatSet, label: minLbl },
      { data: this.heartBeatSet, label: heartBeatLbl },
      { data: this.maxHeartBeatSet, label: maxLbl }
    ];

    this.chart2Datasets = [
      { data: this.minBodyTemperatureSet, label: minLbl },
      { data: this.bodyTemperatureSet, label: temperatureLbl },
      { data: this.maxBodyTemperatureSet, label: maxLbl },
    ];

    this.chart3Datasets = [
      { data: this.minPulseSet, label: minLbl },
      { data: this.pulseSet, label: pulseLbl },
      { data: this.maxPulseSet, label: maxLbl },
    ];
  }

  loadMinMaxValues(lenght: number) {
    let minHeartBeat = 60;
    let maxHeartBeat = 100;

    let minBodyTemperature = 35.9;
    let maxBodyTemperature = 37.2;

    let minPulse = 60;
    let maxPulse = 80;

    this.iotDataService.getFixedIotData(this.teamMember.id).subscribe(res => {
      if (res) {
        minHeartBeat = this.getMin(res.heartBeat - 10, minHeartBeat);
        maxHeartBeat = this.getMax(res.heartBeat + 10, maxHeartBeat);

        minPulse = this.getMin(res.pulse - 5, minPulse);
        maxPulse = this.getMax(res.pulse + 5, maxPulse);
      }

      this.fillDataSet(this.minHeartBeatSet, lenght, minHeartBeat);
      this.fillDataSet(this.maxHeartBeatSet, lenght, maxHeartBeat);

      this.fillDataSet(this.minBodyTemperatureSet, lenght, minBodyTemperature);
      this.fillDataSet(this.maxBodyTemperatureSet, lenght, maxBodyTemperature);

      this.fillDataSet(this.minPulseSet, lenght, minPulse);
      this.fillDataSet(this.maxPulseSet, lenght, maxPulse);
    });
  }

  fillDataSet(dataSet: number[], length: number, value: number) {
    for (let i = 0; i < length; ++i) {
      dataSet[i] = value;
    }
  }

  getMin(a: number, b: number): number {
    return a < b ? a : b;
  }

  getMax(a: number, b: number): number {
    return a > b ? a : b;
  }

  heartBeatSet: number[] = [];
  bodyTemperatureSet: number[] = [];
  pulseSet: number[] = [];
  dateSet: string[] = [];

  minHeartBeatSet: number[] = [];
  maxHeartBeatSet: number[] = [];

  minBodyTemperatureSet: number[] = [];
  maxBodyTemperatureSet: number[] = [];

  minPulseSet: number[] = [];
  maxPulseSet: number[] = [];

  chartType = 'line';

  chart1Datasets = [
    { data: this.minHeartBeatSet, label: 'min' },
    { data: this.heartBeatSet, label: 'Heart beat' },
    { data: this.maxHeartBeatSet, label: 'max' }
  ];

  chart2Datasets = [
    { data: this.minBodyTemperatureSet, label: 'min' },
    { data: this.bodyTemperatureSet, label: 'Temperature' },
    { data: this.maxBodyTemperatureSet, label: 'max' },
  ];

  chart3Datasets = [
    { data: this.minPulseSet, label: 'min' },
    { data: this.pulseSet, label: 'Pulse' },
    { data: this.maxPulseSet, label: 'max' },
  ];

  chartLabels = this.dateSet;
  chart1Colors = [
    {
      borderColor: 'rgba(225, 0, 0, .7)',
      borderWidth: 2
    },
    {
      backgroundColor: 'rgba(0, 137, 132, .2)',
      borderColor: 'rgba(0, 10, 130, .7)',
      borderWidth: 2,
    },
    {
      borderColor: 'rgba(225, 0, 0, .7)',
      borderWidth: 2,
    }
  ];

  chartOptions: any = {
    responsive: true
  };
}
