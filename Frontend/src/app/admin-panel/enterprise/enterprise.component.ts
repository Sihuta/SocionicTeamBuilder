import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Enterprise } from './enterprise';
import { EnterpriseService } from './enterprise.service';
import { NewEnterpriseComponent } from './new-enterprise/new-enterprise.component';

@Component({
  selector: 'app-enterprise',
  templateUrl: './enterprise.component.html',
  styleUrls: ['./enterprise.component.css'],
})
export class EnterpriseComponent implements OnInit {
  enterprises: Enterprise[] = [];

  constructor(
    private readonly router: Router,
    private readonly service: EnterpriseService
  ) {}

  ngOnInit(): void {
    this.loadEnterprises();
  }

  loadEnterprises() {
    this.service.getEnterprises()
      .subscribe((res) => this.enterprises = res);
  }

  edit($event: any) {
    let id = $event.target.name;
    let enterprise = this.enterprises.find(e => e.id == id);
    console.log(enterprise);

    if (enterprise) {
      this.service.setEnterprise(enterprise);
      NewEnterpriseComponent.editMode = true;
      this.router.navigate(['/admin-panel/enterprise/new']);
    }
  }

  delete($event: any) {
    let id = $event.target.name;
    this.service.deleteEnterprise(id).subscribe( 
      (data) =>{
        console.log(data);
        this.ngOnInit();
      }
    ),
    (error: any) => {
      console.log(error);
    }
  }

  add() {
    NewEnterpriseComponent.editMode = false;
    this.router.navigate(['/admin-panel/enterprise/new']);
  }
}