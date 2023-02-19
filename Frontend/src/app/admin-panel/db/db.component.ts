import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { DbService } from './db.service';

@Component({
  selector: 'app-db',
  templateUrl: './db.component.html',
  styleUrls: ['./db.component.css']
})

export class DbComponent implements OnInit {
  fileName: string = "";
  
  formIsValid: boolean = true;

  success: boolean = true;
  successMessage: string = "";
  errorMessage: string = "";

  files: string[] = [];

  constructor(
    public readonly translate: TranslateService,
    private readonly service: DbService
  ) { }

  ngOnInit(): void {
    this.loadBackupFiles();
  }

  loadBackupFiles() {
    this.service.getBackupFiles().subscribe(res => this.files = res);
  }

  restore() {
    this.formIsValid = this.fileName != "";
    if (this.formIsValid) {
      this.service.restore(this.fileName)
      .subscribe((res) => {
        this.translate.stream('ADMIN_PANEL.DB.RESTORE_MSG', {file: res}).subscribe(msg => this.successMessage = msg);
        this.success = true;
        console.log(this.fileName);
      }, () => {
        this.translate.stream('ADMIN_PANEL.DB.ERR_MSG').subscribe(msg => this.errorMessage = msg);
        this.success = false;
     });
    }
  }

  backup() {
    this.service.backup()
      .subscribe((res) => {
        this.translate.stream('ADMIN_PANEL.DB.BACKUP_MSG', {file: res}).subscribe(msg => this.successMessage = msg);
        this.success = true;
        console.log(this.fileName);
      }, () => {
        this.translate.stream('ADMIN_PANEL.DB.ERR_MSG').subscribe(msg => this.errorMessage = msg);
        this.success = false;
     });

     this.ngOnInit();
  }
}
