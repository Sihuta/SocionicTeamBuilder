import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DbComponent } from './db.component';

describe('DbComponent', () => {
  let component: DbComponent;
  let fixture: ComponentFixture<DbComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DbComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DbComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
