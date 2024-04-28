import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubcategoriesByParentContainerComponent } from './subcategories-by-parent-container.component';

describe('SubcategoriesByParentContainerComponent', () => {
  let component: SubcategoriesByParentContainerComponent;
  let fixture: ComponentFixture<SubcategoriesByParentContainerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SubcategoriesByParentContainerComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SubcategoriesByParentContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
