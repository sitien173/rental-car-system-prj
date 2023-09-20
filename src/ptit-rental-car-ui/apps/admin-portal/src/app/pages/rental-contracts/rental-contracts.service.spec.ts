import {TestBed} from '@angular/core/testing';

import {RentalContractsService} from './rental-contracts.service';

describe('RentalContractsService', () => {
  let service: RentalContractsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RentalContractsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
