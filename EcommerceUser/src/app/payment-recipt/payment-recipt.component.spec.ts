import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PaymentReciptComponent } from './payment-recipt.component';

describe('PaymentReciptComponent', () => {
  let component: PaymentReciptComponent;
  let fixture: ComponentFixture<PaymentReciptComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PaymentReciptComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PaymentReciptComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
