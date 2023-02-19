import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { EnterpriseService } from 'src/app/admin-panel/enterprise/enterprise.service';
import { Employee } from '../../employee/employee';
import { EmployeeService } from '../../employee/employee.service';
import { TeamMemberService } from './team-member.service';
import { TeamMember } from './teamMember';

@Component({
  selector: 'app-team-member',
  templateUrl: './team-member.component.html',
  styleUrls: ['./team-member.component.css']
})
export class TeamMemberComponent implements OnInit {

  teamMembers: TeamMember[] = [];
  employees: Employee[] = [];

  constructor(
    private readonly router: Router,
    private readonly service: TeamMemberService,
    private readonly employeeService: EmployeeService,
    private readonly enterpriseService: EnterpriseService
  ) { }

  ngOnInit(): void {
    this.loadData();
  }

  savePosition($event: any) {
    console.log($event.target.name);
    console.log(this.teamMembers);
  }

  loadData() {
    let taskId = localStorage.getItem('taskId');
    console.log(taskId);

    if (taskId) {
      this.service.getMembersByTaskId(taskId).subscribe(res => {
        this.teamMembers = res;
        //console.log(res);
        this.loadEmployees();
      });
    }
  }

  loadEmployees() {
    let userId = localStorage.getItem("userId");
    if (userId) {
      this.enterpriseService.getEnterpriseByUserId(userId).subscribe(enterprise => {
        console.log(enterprise);
        this.employeeService.getEmployees(enterprise).subscribe(res => {
          this.employees = res;
        });
      });
    }
  }

  getFullName(employeeId: number) {
    return this.employees.find(e => e.id == employeeId)?.fullName;
  }

  getWorkingProfile(employeeId: number) {
    return this.employees.find(e => e.id == employeeId)?.workingProfile;
  }

  save() {
    console.log(this.teamMembers)
    this.teamMembers.forEach(tm => {
      this.service.updateTeamMember(tm).subscribe(() => this.router.navigate(['/team-creator-panel/task']));
    });
  }
}
