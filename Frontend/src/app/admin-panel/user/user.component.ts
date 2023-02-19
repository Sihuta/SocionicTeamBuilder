import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SessionService } from 'src/app/session.service';
import { NewUserComponent } from './new-user/new-user.component';
import { User } from './user';
import { UserService } from './user.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  users: User[] = [];

  constructor(
    private router: Router,
    private readonly service: UserService,
    readonly sessionService: SessionService
  ) { }

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers() {
    this.service.getUsers()
      .subscribe((res) => this.users = res);
  }

  edit($event: any) {
    let id = $event.target.name;
    let user = this.users.find(u => u.id == id);
    
    if (user) {
      this.service.setUser(user);
      if (this.checkIfAdminIsUserForEditing(user)) {
        NewUserComponent.editModeOwnAcc = true;
      } else {
        NewUserComponent.editMode = true;
        NewUserComponent.editModeOwnAcc = false;
      }

      this.router.navigate(['/admin-panel/user/new']);
    }
  }

  delete($event: any) {
    let id = $event.target.name;
    this.service.deleteUser(id).subscribe( 
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
    NewUserComponent.editMode = false;
    NewUserComponent.editModeOwnAcc = false;
    this.router.navigate(['/admin-panel/user/new']);
  }

  checkIfAdminIsUserForEditing(user: User) : boolean {
    return user.role == "admin";
  }
}
