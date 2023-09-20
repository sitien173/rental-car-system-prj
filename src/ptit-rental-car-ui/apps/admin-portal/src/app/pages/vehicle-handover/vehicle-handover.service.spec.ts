import {TestBed} from '@angular/core/testing';

import {VehicleHandoverService} from './vehicle-handover.service';

describe('VehicleHandoverService', () => {
  let service: VehicleHandoverService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(VehicleHandoverService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
