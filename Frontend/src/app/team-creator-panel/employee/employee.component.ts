import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { EnterpriseService } from 'src/app/admin-panel/enterprise/enterprise.service';
import { Employee } from './employee';
import { EmployeeService } from './employee.service';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})
export class EmployeeComponent implements OnInit {

  employees: Employee[] = [];
  enterprise: string = "";

  constructor(
    private readonly router: Router,
    private readonly service: EmployeeService,
    private readonly enterpriseService: EnterpriseService
  ) { }

  ngOnInit(): void {
    this.loadEmployees();
  }

  loadEmployees() {
    let userId = localStorage.getItem("userId");
    if (userId) {
      this.enterpriseService.getEnterpriseByUserId(userId).subscribe(res => {
        this.enterprise = res;
        console.log(res);
        this.getEmployees(res);
      });
    }
  }

  private getEmployees(enterprise: string) {
    this.service.getEmployees(enterprise).subscribe(
      res => {
        this.employees = res,
        console.log(this.employees);
        console.log(res);
      }),
    (err: any) => console.log(err);
  }

  add() {
    this.router.navigate(['/team-creator-panel/employee/new']);
  }
}
