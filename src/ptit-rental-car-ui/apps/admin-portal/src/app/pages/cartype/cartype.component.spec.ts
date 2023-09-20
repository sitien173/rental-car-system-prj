import {ComponentFixture, TestBed} from '@angular/core/testing';

import {CartypeComponent} from './cartype.component';

describe('CartypeComponent', () => {
  let component: CartypeComponent;
  let fixture: ComponentFixture<CartypeComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CartypeComponent]
    });
    fixture = TestBed.createComponent(CartypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
