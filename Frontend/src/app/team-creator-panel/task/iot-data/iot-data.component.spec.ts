import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IotDataComponent } from './iot-data.component';

describe('IotDataComponent', () => {
  let component: IotDataComponent;
  let fixture: ComponentFixture<IotDataComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IotDataComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IotDataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
