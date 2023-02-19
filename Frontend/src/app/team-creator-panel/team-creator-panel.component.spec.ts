import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TeamCreatorPanelComponent } from './team-creator-panel.component';

describe('TeamCreatorPanelComponent', () => {
  let component: TeamCreatorPanelComponent;
  let fixture: ComponentFixture<TeamCreatorPanelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TeamCreatorPanelComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TeamCreatorPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
