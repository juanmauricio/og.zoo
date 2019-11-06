import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InfirmariesComponent } from './infirmaries.component';

describe('InfirmariesComponent', () => {
  let component: InfirmariesComponent;
  let fixture: ComponentFixture<InfirmariesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InfirmariesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InfirmariesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
