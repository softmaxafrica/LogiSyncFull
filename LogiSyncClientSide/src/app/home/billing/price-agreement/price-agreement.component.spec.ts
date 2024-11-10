import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PriceAgreementComponent } from './price-agreement.component';

describe('PriceAgreementComponent', () => {
  let component: PriceAgreementComponent;
  let fixture: ComponentFixture<PriceAgreementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PriceAgreementComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PriceAgreementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
