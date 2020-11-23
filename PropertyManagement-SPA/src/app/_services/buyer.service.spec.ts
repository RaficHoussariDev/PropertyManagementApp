/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { BuyerService } from './buyer.service';

describe('Service: Buyer', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [BuyerService]
    });
  });

  it('should ...', inject([BuyerService], (service: BuyerService) => {
    expect(service).toBeTruthy();
  }));
});
