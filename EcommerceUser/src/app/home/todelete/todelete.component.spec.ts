import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TodeleteComponent } from './todelete.component';

describe('TodeleteComponent', () => {
  let component: TodeleteComponent;
  let fixture: ComponentFixture<TodeleteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TodeleteComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TodeleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
