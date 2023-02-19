import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-team-creator-panel',
  templateUrl: './team-creator-panel.component.html',
  styleUrls: ['./team-creator-panel.component.css']
})
export class TeamCreatorPanelComponent implements OnInit {
  baseUrl: string = "team-creator-panel";

  employeeUrl: string = this.baseUrl + "/employee";
  taskUrl: string = this.baseUrl + "/task";
  teamUrl: string = this.baseUrl + "/team";
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
