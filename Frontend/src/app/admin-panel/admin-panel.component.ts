import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})

export class AdminPanelComponent implements OnInit {
  baseUrl: string = "/admin-panel";

  mainUrl: string = this.baseUrl + "/main";
  enterpriseUrl: string = this.baseUrl + "/enterprise";
  userUrl: string = this.baseUrl + "/user";
  dbUrl: string = this.baseUrl + "/db";

  selectedLang: string = "";

  constructor(readonly translate: TranslateService) { }

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
}
