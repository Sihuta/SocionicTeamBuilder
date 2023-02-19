import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { EmployeeService } from '../../employee/employee.service';
import { TeamMemberService } from '../team-member/team-member.service';
import { TeamMember } from '../team-member/teamMember';
import { Feedback } from './feedback';
import { FeedbackService } from './feedback.service';

@Component({
  selector: 'app-feedback',
  templateUrl: './feedback.component.html',
  styleUrls: ['./feedback.component.css']
})
export class FeedbackComponent implements OnInit {

  teamId: number;
  employeeId: number;

  employeeName: string = "";
  teamMember: TeamMember = new TeamMember(0, 0, 0, 0, "");

  feedbacks: Feedback[] = [];

  minDate: Date = new Date("5/1/22");
  maxDate: Date = new Date("5/27/22");

  startDate: Date = new Date();
  endDate: Date = new Date();

  constructor(
    private readonly employeeService: EmployeeService,
    private readonly teamMemberService: TeamMemberService,
    private readonly feedbackService: FeedbackService,

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
      this.loadFeedbackByDateRange(this.startDate.toDateString(), this.endDate.toDateString());
    }
  }

  loadData() {
    this.loadName();
    this.loadFeedback();
  }

  loadName() {
    this.employeeService.getEmployee(this.employeeId)
    .subscribe(res => this.employeeName = res.fullName);
  }

  loadFeedback() {
    this.teamMemberService.getByTeamAndEmployeeId(this.teamId, this.employeeId)
    .subscribe(res => {
      this.teamMember = res;

      this.feedbackService.getFeedback(this.teamMember.id)
      .subscribe(res => {
        this.feedbacks = res;

        if (res.length > 0) {
          this.maxDate = res[0].dateTime;
          this.minDate = res[res.length - 1].dateTime;
        }

        // this.startDate = this.minDate;
        // this.endDate = this.maxDate;
      });
    });
  }

  loadFeedbackByDateRange(startDate: string, endDate: string) {
    this.feedbackService.getFeedbackByDateRange(this.teamMember.id, startDate, endDate)
    .subscribe(res => {this.feedbacks = res;});
  }
}
