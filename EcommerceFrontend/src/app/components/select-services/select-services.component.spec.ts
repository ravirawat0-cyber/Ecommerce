import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectServicesComponent } from './select-services.component';

describe('SelectServicesComponent', () => {
  let component: SelectServicesComponent;
  let fixture: ComponentFixture<SelectServicesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SelectServicesComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SelectServicesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
