import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { FormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { EnterpriseComponent } from './admin-panel/enterprise/enterprise.component';
import { UserComponent } from './admin-panel/user/user.component';
import { DbComponent } from './admin-panel/db/db.component';
import { NewEnterpriseComponent } from './admin-panel/enterprise/new-enterprise/new-enterprise.component';
import { EnterpriseService } from './admin-panel/enterprise/enterprise.service';
import { NewUserComponent } from './admin-panel/user/new-user/new-user.component';
import { UserService } from './admin-panel/user/user.service';
import { SessionService } from './session.service';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { TeamCreatorPanelComponent } from './team-creator-panel/team-creator-panel.component';
import { MainComponent } from './admin-panel/main/main.component';
import { EmployeeComponent } from './team-creator-panel/employee/employee.component';
import { TaskComponent } from './team-creator-panel/task/task.component';
import { TeamComponent } from './team-creator-panel/task/team/team.component';
import { NewEmployeeComponent } from './team-creator-panel/employee/new-employee/new-employee.component';
import { NewTaskComponent } from './team-creator-panel/task/new-task/new-task.component';
import { TeamMemberComponent } from './team-creator-panel/task/team-member/team-member.component';
import { TaskService } from './team-creator-panel/task/task.service';
import { AuthInterceptor } from './auth.interceptor';
import { IotDataComponent } from './team-creator-panel/task/iot-data/iot-data.component';
import { FeedbackComponent } from './team-creator-panel/task/feedback/feedback.component';

import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ChartsModule, WavesModule } from 'angular-bootstrap-md'

const adminPanelRoutes: Routes = [
  { path: 'main', component: MainComponent },
  { path: 'enterprise', component: EnterpriseComponent },
  { path: 'enterprise/new', component: NewEnterpriseComponent},
  { path: 'user', component: UserComponent},
  { path: 'user/new', component: NewUserComponent},
  { path: 'db', component: DbComponent}
];

const teamCreatorPanelRoutes: Routes = [
  { path: 'employee', component: EmployeeComponent },
  { path: 'employee/new', component: NewEmployeeComponent },
  { path: 'task', component: TaskComponent},
  { path: 'task/team-member', component: TeamMemberComponent},
  { path: 'task/team', component: TeamComponent},
  { path: 'task/team/:id/iot/:employeeId', component: IotDataComponent},
  { path: 'task/team/:id/feedback/:employeeId', component: FeedbackComponent},
  { path: 'task/new', component: NewTaskComponent},
  { path: 'db', component: DbComponent}
];

const appRoutes: Routes = [
  { path: '', component: LoginComponent },
  {
    path: 'admin-panel',
    component: AdminPanelComponent,
    children: adminPanelRoutes },
  {
    path: 'team-creator-panel',
    component: TeamCreatorPanelComponent,
    children: teamCreatorPanelRoutes}
];

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}

@NgModule({
declarations: [
  AppComponent,
  LoginComponent,
  AdminPanelComponent,
  EnterpriseComponent,
  MainComponent,
  UserComponent,
  DbComponent,
  NewEnterpriseComponent,
  NewUserComponent,
  TeamCreatorPanelComponent,
  EmployeeComponent,
  NewEmployeeComponent,
  TaskComponent,
  NewTaskComponent,
  TeamMemberComponent,
  TeamComponent,
  IotDataComponent,
  FeedbackComponent
],
imports: [
  BrowserAnimationsModule,
  MatFormFieldModule,
  MatDatepickerModule,
  MatNativeDateModule,

  ChartsModule,
  WavesModule,

  BrowserModule,
  HttpClientModule,
  FormsModule,
  RouterModule.forRoot(appRoutes),
  TranslateModule.forRoot({
    loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
    },
    defaultLanguage: 'en'
  })
],
 providers: [
  EnterpriseService,
  UserService,
  SessionService,
  TaskService,
  {
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true
  }
],
 bootstrap: [AppComponent]
})

export class AppModule { }
