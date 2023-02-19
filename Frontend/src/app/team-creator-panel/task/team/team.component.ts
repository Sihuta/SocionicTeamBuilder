import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { EnterpriseService } from 'src/app/admin-panel/enterprise/enterprise.service';
import { Employee } from '../../employee/employee';
import { EmployeeService } from '../../employee/employee.service';
import { Task } from '../task';
import { TaskService } from '../task.service';
import { TeamMemberService } from '../team-member/team-member.service';
import { TeamMember } from '../team-member/teamMember';
import { CreatedGroupOfTeams } from './createdGroupOfTeam';
import { CreatedTeam } from './createdTeam';
import { Team } from './team';
import { TeamService } from './team.service';
import { TeamFullInfo } from './teamFullInfo';

@Component({
  selector: 'app-team',
  templateUrl: './team.component.html',
  styleUrls: ['./team.component.css']
})
export class TeamComponent implements OnInit {

  useBlacklist: boolean = false;
  createdGroupOfTeams: CreatedGroupOfTeams[] = [];

  tasks: Task[] = [];
  employees: Employee[] = [];
  teamMembers: TeamMember[] = [];

  taskId: string | null = "";
  teamsCreated: boolean = false;

  teams: TeamFullInfo[] = [];
  task: Task = new Task(0, "", 0, 0, 0, false, false);

  constructor(
    private readonly service: TeamService,
    private readonly taskService: TaskService,
    private readonly translate: TranslateService,
    private readonly employeeService: EmployeeService,
    private readonly teamMemberService: TeamMemberService,
    private readonly enterpriseService: EnterpriseService
  ) { }

  ngOnInit(): void {
    this.loadData();
  }

  blacklistCheckboxChanged($event: any): void {
    let checked = $event.target.checked;
    console.log(checked);

    this.loadTeamBuildingResult();
  }

  rebuild() {
    this.teamsCreated = false;
    this.service.getTeams().subscribe(res => {
      this.createdGroupOfTeams = res;
      this.loadEmployees();
    });
  }

  approve($event: any) {
    this.service.deleteTeamsByTaskId();

    let ind = $event.target.name;
    let teams = this.createdGroupOfTeams[ind];

    this.loadTeamMembers();

    teams.createdTeams.forEach(t => {
      let team = new Team(0, t.wayOfBuilding, true, false);
      this.service.createTeam(team).subscribe(teamId => {
        console.log(teamId);
        this.teamMembers.forEach(tm => {
          if (t.employeeIdList.includes(tm.employeeId)) {
            console.log(tm);
            tm.teamId = teamId;
            this.teamMemberService.updateTeamMember(tm).subscribe();
          }
        });
      });
    });

    this.taskService.getTask().subscribe(task => {
      task.teamsCreated = true;
      this.taskService.updateTask(task).subscribe();
    });
    
    this.teamsCreated = true;
    this.ngOnInit();
  }

  loadTeamMembers() {
    this.teamMemberService.getMembersByTaskId(localStorage.getItem('taskId') + '')
      .subscribe(res => this.teamMembers = res);
  }

  loadData() {
    this.taskService.getTask().subscribe(task => {
      this.teamsCreated = task.teamsCreated;
      console.log(task);
      if (this.teamsCreated) {
        this.loadTeams();
      } else {
       this.loadTeamBuildingResult();
      }

      this.loadEmployees();
    });
  }

  loadTeamBuildingResult() {
    if (this.useBlacklist) {
      this.service.getTeamsByBlacklist().subscribe(res => {
        this.createdGroupOfTeams = res;
      });
    } else {
      this.service.getTeams().subscribe(res => {
        this.createdGroupOfTeams = res;
      });
    }
  }

  loadEmployees() {
    let userId = localStorage.getItem("userId");
    if (userId) {
      this.enterpriseService.getEnterpriseByUserId(userId).subscribe(res => {
        this.employeeService.getEmployees(res).subscribe(res => {
          this.employees = res;
        });
      });
    }
  }

  loadTeams() {
    this.service.getCreatedTeams().subscribe(res => {
      this.teams = res;
      console.log(res);
      let taskId = localStorage.getItem('taskId') + "";
      this.teamMemberService.getMembersByTaskId(taskId).subscribe(res => this.teamMembers = res);
    });
  }

  getTeamNumber(team: TeamFullInfo) {
    return this.teams.indexOf(team) + 1;
  }

  getVariantNumber(groupOfTeams: CreatedGroupOfTeams) {
    return this.createdGroupOfTeams.indexOf(groupOfTeams) + 1;
  }

  getVariantDescription(groupOfTeams: CreatedGroupOfTeams) {
    let key = 'TEAM-CREATOR_PANEL.TEAM.CODE.';

    switch (groupOfTeams.descriptionCode) {
      case "diffWays":
        key += "DIFF_WAYS";
        break;
      case "bestIfDeadline":
        key += "BEST_IF_DEADLINE";
        break;
      case "bestOne":
        key += "BEST_ONE";
        break;
      case "worstOne":
        key += "WORST_ONE";
        break;
    }

    let label = "";
    this.translate.stream(key).subscribe(res => label = res);
    return label;
  }

  isBestVariant(groupOfTeams: CreatedGroupOfTeams) {
    let timeIsLimited = localStorage.getItem('timeIsLimited');

    switch (groupOfTeams.descriptionCode) {
      case "bestIfDeadline":
        if (timeIsLimited == "true") {
          return true;
        }
        if (!this.createdGroupOfTeams.find(gt => gt.descriptionCode == 'bestOne')) {
          return true;
        }
        break;
      case "bestOne":
        if (timeIsLimited == "false") {
          return true;
        }
        if (!this.createdGroupOfTeams.find(gt => gt.descriptionCode == 'bestIfDeadline')) {
          return true;
        }
        break;
    }

    return false;
  }

  getTeamIndex(groupOfTeams: CreatedGroupOfTeams, createdTeam: CreatedTeam) {
    let gt = this.createdGroupOfTeams.find(gt => gt == groupOfTeams);
    if (gt) {
      return gt.createdTeams.indexOf(createdTeam) + 1;
    }
    return -1;
  }

  getEmployeeInfo(id: number) {
    let emp = this.employees.find(e => e.id == id);
    if (emp) {
      return emp.fullName + " - " + emp.socionicType;
    }
    return id;
  }

  getTeamMemberInfo(empId: number) {
    let emp = this.employees.find(e => e.id == empId);
    let tm = this.teamMembers.find(t => t.employeeId == empId);
    
    if (emp && tm) {
      return emp.fullName + " - " + tm.position;
    }
    return empId;
  }
}