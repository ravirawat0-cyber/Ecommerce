import { Component } from '@angular/core';
import { ICategoryRes } from '../../../models/category.model';
import { CategoryService } from '../../../services/category.service';
import { response } from 'express';
import { error } from 'console';
import { json } from 'stream/consumers';
import { ISubcategoryForm } from '../../../models/subcategory.model';
import { SubcategoryService } from '../../../services/subcategory.service';

@Component({
  selector: 'app-subcategoryform',
  templateUrl: './subcategoryform.component.html',
  styleUrl: './subcategoryform.component.scss',
})
export class SubcategoryformComponent {
  categories: ICategoryRes[] = [];
  subcategoryName!: string | null;
  selectedParentCategoryId!: number;

  constructor(
    private categoryService: CategoryService,
    private subcategoryService: SubcategoryService
  ) {}

  ngOnInit(): void {
    this.fetchCategories();
  }

  fetchCategories() {
    this.categoryService.getCategory().subscribe(
      (response) => {
        this.categories = response.data;
      },
      (error) => {
        console.error('Error fetching categories:', error);
      }
    );
  }

  handleSubmit() {
    if (this.subcategoryName) {
      const subcategory: ISubcategoryForm = {
        name: this.subcategoryName,
        parentCategoryId: this.selectedParentCategoryId,
      };

      this.subcategoryService.addSubcategory(subcategory).subscribe((res) => {
        console.log(res);
        this.subcategoryName = null;
      });
    }
  }

  onSelectCategory(event: any) {
    this.selectedParentCategoryId = event.target.value;
  }
}
