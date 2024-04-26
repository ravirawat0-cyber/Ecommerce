import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DataToolMainComponent } from './data-tool-main.component';

describe('DataToolMainComponent', () => {
  let component: DataToolMainComponent;
  let fixture: ComponentFixture<DataToolMainComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DataToolMainComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DataToolMainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
