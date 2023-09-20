import {ComponentFixture, TestBed} from '@angular/core/testing';

import {RentalRequestComponent} from './rental-request.component';

describe('RentalRequestComponent', () => {
  let component: RentalRequestComponent;
  let fixture: ComponentFixture<RentalRequestComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RentalRequestComponent]
    });
    fixture = TestBed.createComponent(RentalRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
