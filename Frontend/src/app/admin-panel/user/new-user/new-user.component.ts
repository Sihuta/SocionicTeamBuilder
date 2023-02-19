import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { Enterprise } from '../../enterprise/enterprise';
import { EnterpriseService } from '../../enterprise/enterprise.service';
import { User } from '../user';
import { UserService } from '../user.service';

@Component({
  selector: 'app-new-user',
  templateUrl: './new-user.component.html',
  styleUrls: ['./new-user.component.css']
})

export class NewUserComponent implements OnInit {
  public static editMode: boolean = false;
  public static editModeOwnAcc: boolean = false;
  
  enterprises: Enterprise[] = [];
  enterpriseId: number = 0;
  admin: boolean = true;

  user: User = this.getDefaultUser();

  changePass: boolean = false;
  oldPass: string = "";
  newPass1: string = "";
  newPass2: string = "";

  newPassesDiferent: boolean = false;
  oldPassSameAsNew: boolean = false;

  label: string = "";
  errorMessage: string = "";

  formIsValid: boolean = true;
  loginIsUnique: boolean = true;

  constructor(
    public readonly translate: TranslateService,
    private readonly router: Router,
    private readonly service: UserService,
    private readonly enterpriseService: EnterpriseService
  ) { }

  getDefaultUser() {
    return new User(0, "", "", "", "", 0);
  }

  get staticEditMode() {
    return NewUserComponent.editMode;
  }

  get staticEditModeOwnAcc() {
    return NewUserComponent.editModeOwnAcc;
  }

  ngOnInit(): void {
    this.loadEnterprises();

    if (NewUserComponent.editMode || NewUserComponent.editModeOwnAcc) {
      let user = this.service.getUser();
      this.initialize(user);
    } else {
      this.translate.stream('ADMIN_PANEL.USER.ADD_LBL').subscribe(res => this.label = res);
    }
  }

  initialize(user: User)  {
    this.translate.stream('ADMIN_PANEL.USER.EDIT_LBL').subscribe(res => this.label = res);
    this.user = new User(
      user.id,
      user.login,
      user.password,
      user.email,
      user.role,
      0
    );
  }

  loadEnterprises() {
    this.enterpriseService.getEnterprises().subscribe(res => this.enterprises = res);
  }

  changeRole($event: any) {
    let newRole = $event;
    if (newRole == "admin") {
      this.user.enterpriseId = 0;
      this.admin = true;
    } else {
      this.admin = false;
    }
  }

  submit() {
    if (this.user.role == "" || this.user.email == "" || this.user.login == "") {
      this.formIsValid = false;
      return;
    }

    if (NewUserComponent.editModeOwnAcc) {
      this.updateOwn();
    }
    else if (NewUserComponent.editMode) {
      this.update();
    } else {
      this.create();
    }
  }

  updateOwn() {
    if (this.checkInputFields()) {
      this.update();
    }
  }

  checkInputFields(): boolean {
    if (this.changePass) {
      if (this.oldPass == "" || this.newPass1 == "" || this.newPass2 == "") {
        this.formIsValid = false;
        return false;
      }
      if (this.newPass1 != this.newPass2) {
        this.formIsValid = true;
        this.newPassesDiferent = true;
        return false;
      }
      if (this.oldPass == this.newPass1) {
        this.formIsValid = true;
        this.newPassesDiferent = true;
        this.oldPassSameAsNew = true;
        return false;
      }
    }

    this.formIsValid = true;
    this.newPassesDiferent = false;
    this.oldPassSameAsNew = false;

    this.user.password = this.newPass1;
    return true;
  }

  update() {
    this.service.updateUser(this.user).subscribe(res => {
      console.log(res);
      this.router.navigate(['/admin-panel/user']);
    });
  }

  create() {
    this.service.createUser(this.user)
    .subscribe(res => {
      console.log(res);
      this.router.navigate(['/admin-panel/user']);
    }, () => {
      this.loginIsUnique = false;
    });
  }
}