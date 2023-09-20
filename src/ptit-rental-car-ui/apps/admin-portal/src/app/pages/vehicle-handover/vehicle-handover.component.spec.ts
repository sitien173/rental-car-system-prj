import {ComponentFixture, TestBed} from '@angular/core/testing';

import {VehicleHandoverComponent} from './vehicle-handover.component';

describe('VehicleHandoverComponent', () => {
  let component: VehicleHandoverComponent;
  let fixture: ComponentFixture<VehicleHandoverComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [VehicleHandoverComponent]
    });
    fixture = TestBed.createComponent(VehicleHandoverComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
