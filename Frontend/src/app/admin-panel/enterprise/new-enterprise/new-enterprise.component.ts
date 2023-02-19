import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { Enterprise } from '../enterprise';
import { EnterpriseService } from '../enterprise.service';

@Component({
  selector: 'app-new-enterprise',
  templateUrl: './new-enterprise.component.html',
  styleUrls: ['./new-enterprise.component.css'],
})
export class NewEnterpriseComponent implements OnInit {
  public static editMode: boolean = false;
  
  enterprise: Enterprise = {
    id: 0,
    name: "",
    location: "",
    activity: ""
  };

  label: string = "";
  formIsValid: boolean = true;

  constructor(
    public readonly translate: TranslateService,
    private readonly router: Router,
    private readonly service: EnterpriseService) { }

  ngOnInit(): void {
    if (NewEnterpriseComponent.editMode) {
      let enterprise = this.service.getEnterprise();
      this.initialize(enterprise);
    } else {
      this.translate.stream('ADMIN_PANEL.ENTERPRISE.ADD_LBL').subscribe(res => this.label = res);
    }
  }

  initialize(enterprise: Enterprise)  {
    this.translate.stream('ADMIN_PANEL.ENTERPRISE.EDIT_LBL').subscribe(res => this.label = res);
    this.enterprise = enterprise;
  }

  submit() {
    if (this.enterprise.name == "" || this.enterprise.location == "" || this.enterprise.activity == "") {
      this.formIsValid = false;
      return;
    }

    if (NewEnterpriseComponent.editMode) {
      this.service.updateEnterprise(this.enterprise).subscribe(res => console.log(res));
    } else {
      this.service.createEnterprise(this.enterprise).subscribe(res => console.log(res));
    }

    this.router.navigate(['/admin-panel/enterprise']);
  }

  equals(e1: Enterprise, e2: Enterprise) : boolean {
    console.log("check equality");
    return e1.activity === e2.activity
      && e1.location === e2.location
      && e1.name === e2.name;
  }
}