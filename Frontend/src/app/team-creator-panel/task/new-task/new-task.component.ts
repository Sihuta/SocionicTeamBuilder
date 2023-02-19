import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { EnterpriseService } from 'src/app/admin-panel/enterprise/enterprise.service';
import { Employee } from '../../employee/employee';
import { EmployeeService } from '../../employee/employee.service';
import { Task } from '../task';
import { TaskService } from '../task.service';
import { TeamMemberService } from '../team-member/team-member.service';
import { TeamMember } from '../team-member/teamMember';

@Component({
  selector: 'app-new-task',
  templateUrl: './new-task.component.html',
  styleUrls: ['./new-task.component.css']
})
export class NewTaskComponent implements OnInit {

  task: Task = new Task(0, "", 0, 1, 1, false, false);

  teamMembers: TeamMember[] = [];
  employees: Employee[] = [];
  enterprise: string = "";

  employeeCount: number = this.task.employeeCount;

  formIsValid: boolean = true;
  taskParamsValid: boolean = true;
  isUnique: boolean = true;
  hasValues: boolean = true;

  constructor(
    private readonly router: Router,
    private readonly service: TaskService,
    private readonly teamMemberService: TeamMemberService,
    private readonly employeeService: EmployeeService,
    private readonly enterpriseService: EnterpriseService
  ) { }

  ngOnInit(): void {
    this.loadEmployees();
  }

  getEmployeeInfo(id: number) {
    let e = this.employees.find(e => e.id == id);
    if (e) {
      return e.fullName + " - " + e.workingProfile; 
    }
    return id;
  }

  saveEmpCnt($event: any) {
    let diff;
    if ($event > this.employeeCount) {
      diff = $event - this.employeeCount;
      for (let i = 0; i < diff; ++i) {
        this.teamMembers.push(new TeamMember(0, 0, 0, this.task.id, ""));
      }
    } else {
      diff = this.employeeCount - $event;
      for (let i = 0; i < diff; ++i) {
        this.teamMembers.pop();
      }
    }

    this.employeeCount = $event;
  }

  loadEmployees() {
    let userId = localStorage.getItem("userId");
    if (userId) {
      this.enterpriseService.getEnterpriseByUserId(userId).subscribe(res => {
        this.enterprise = res;
        this.getEmployees(res);
      });
    }
  }

  getEmployees(enterprise: string) {
    this.employeeService.getEmployees(enterprise).subscribe(res => {
      res.forEach(e => {
        if (e.socionicType != 'Undefined') {
          this.employees.push(e);
        }
      });
    });
  }

  submit() {
    if (this.checkValidity()) {
      this.service.createTask(this.task).subscribe(res => {
        console.log("taskId = " + res);
        this.createMembers(res);
      });
    }
  }

  createMembers(taskId: number) {
    this.teamMembers.forEach(tm => {
      tm.taskId = taskId;
      this.teamMemberService.createTeamMember(tm).subscribe();
      this.router.navigate(['/team-creator-panel/task']);
    });
  }

  checkValidity(): boolean {
    if (this.task.title == "") {
      this.formIsValid = false;
      return false;
    }

    if (this.task.employeeCount / this.task.teamCount < this.task.minTeamSize) {
      this.formIsValid = true;
      //console.log(this.task.employeeCount / this.task.teamCount);
      this.taskParamsValid = false;
      return false;
    }

    for (let tm of this.teamMembers) {
      if (tm.employeeId == 0) {
        this.isUnique = true;
        this.taskParamsValid = true;
        this.formIsValid = true;

        this.hasValues = false;
        return false;
      }      
    }

    this.teamMembers.sort();
    let len = this.teamMembers.length;

    for (let i = 0; i < len; ++i) {
      for (let j = i; j < len; ++j) {
        if (this.teamMembers[i].employeeId == this.teamMembers[j].employeeId && i != j) {
          this.taskParamsValid = true;
          this.formIsValid = true;
          this.hasValues = true;

          this.isUnique = false;
          return false;
        }
      }
    }

    this.isUnique = true;
    this.taskParamsValid = true;
    this.formIsValid = true;
    this.hasValues = true;

    return true;
  }
}
