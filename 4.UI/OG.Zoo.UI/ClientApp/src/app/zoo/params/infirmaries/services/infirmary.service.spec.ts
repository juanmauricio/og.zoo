import { TestBed } from '@angular/core/testing';

import { InfirmaryService } from './infirmary.service';

describe('InfirmaryService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: InfirmaryService = TestBed.get(InfirmaryService);
    expect(service).toBeTruthy();
  });
});
