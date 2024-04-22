import { Component } from '@angular/core';
import { ICategoryForm } from '../../../models/category.model';
import { CategoryService } from '../../../services/category.service';
import { take } from 'rxjs';

@Component({
  selector: 'app-categoryform',
  templateUrl: './categoryform.component.html',
  styleUrl: './categoryform.component.scss',
})
export class CategoryformComponent {
  categoryName!: string | null;

  constructor(private apiService: CategoryService) {}
  handleSubmit() {
    if (this.categoryName) {
      const category: ICategoryForm = {
        name: this.categoryName,
      };

      this.apiService.addCategory(category).subscribe((res) => {
        this.categoryName = null;
      });
    }
  }
}
