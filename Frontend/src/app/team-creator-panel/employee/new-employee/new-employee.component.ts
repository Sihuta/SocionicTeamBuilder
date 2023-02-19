import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { EnterpriseService } from 'src/app/admin-panel/enterprise/enterprise.service';
import { Employee } from '../employee';
import { EmployeeService } from '../employee.service';

@Component({
  selector: 'app-new-employee',
  templateUrl: './new-employee.component.html',
  styleUrls: ['./new-employee.component.css']
})
export class NewEmployeeComponent implements OnInit {

  employee: Employee = new Employee(0, "", "", "", "", "employee", "", "", "");
  formIsValid: boolean = true;
  loginIsUnique: boolean = true;

  constructor(
    private readonly router: Router,
    private readonly service: EmployeeService,
    private readonly enterpriseService: EnterpriseService
  ) { }

  ngOnInit(): void {
    let userId = localStorage.getItem('userId');
    if (userId) {
      this.enterpriseService.getEnterpriseByUserId(userId).subscribe(res => this.employee.enterprise = res);
    }
  }

  submit() {
    if (this.employee.email == "" || this.employee.fullName == ""
    || this.employee.login == "" || this.employee.password == "") {
      this.formIsValid = false;
      return;
    }

    this.formIsValid = true;
    this.register();
  }

  register() {
    this.service.registerEmployee(this.employee)
    .subscribe(res => {
      console.log(res);
      this.router.navigate(['/team-creator-panel/employee']);
    },
    () => this.loginIsUnique = false);
  }
}
