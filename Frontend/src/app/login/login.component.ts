import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { LoginService } from './login.service';
import { LoginData } from './loginData';

@Component({
  selector: 'app-root',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  login: string = "";
  password: string = "";

  formIsValid: boolean = true;
  userNotFound: boolean = false;

  selectedLang: string = "";

  constructor(
    public readonly translate: TranslateService,
    private readonly router: Router,
    private readonly service: LoginService,
  ) { }

  ngOnInit(): void {
    let lang = localStorage.getItem("lang");
    if (lang) {
      this.translate.use(lang);
      this.selectedLang = lang;
    }
  }

  onChange($event: any) {
    let lang = $event.target.value;
    this.translate.use(lang);
    localStorage.setItem("lang", lang);

    console.log(this.selectedLang);
  }

  logIn() {
    if (this.login == "" || this.password == "") {
      this.formIsValid = false;
      return;
    }
    this.formIsValid = true;

    this.service.logIn(this.login, this.password)
      .subscribe( 
        user => {
          console.log(user);
          localStorage.setItem('userId', user.id.toString());
          localStorage.setItem('userRole', user.role);

          this.service.getToken(new LoginData(this.login, this.password)).subscribe(
            token => {
              localStorage.setItem('token', token.accessToken);
              localStorage.setItem('token_expiration', token.expiration.toString());
              console.log(token);
            }
          );
  
          this.chooseNavigation(user.role);
        },
        () => {
          this.userNotFound = true;
        }
      );
  }

  chooseNavigation(role: string) {
    switch (role) {
      case "admin":
        this.router.navigate(['/admin-panel/main']);
        break;
      case "teamCreator":
        this.router.navigate(['/team-creator-panel/task']);
        break;
    }
  }
}
