import {Component} from '@angular/core';

import {CategoryDataService} from "../all-services/category-data.service";
import {CategoryService} from "../../../../services/category.service";
import {ICategoryForm} from "../../../../models/category.model";

@Component({
    selector: 'app-categoryform',
    templateUrl: './categoryform.component.html',
    styleUrl: './categoryform.component.scss',
})
export class CategoryformComponent {
    categoryName: string = '';
    errorMessage: string = '';


    constructor(private apiService: CategoryService,
                private categoryDataService: CategoryDataService,
                ) {
    }


    handleSubmit() {
        if (this.categoryName) {
            const category: ICategoryForm = {
                name: this.categoryName,
            };

            this.apiService.addCategory(category).subscribe(
                (res) => {
                    this.errorMessage = '';
                    this.categoryDataService.notifyCategoryFormSubmitted()
                    this.categoryName = '';
                },
                (error) => {
                    this.errorMessage = error.error;
                    this.categoryName = '';

                }
            );
        }
    }

    get isFormFilled(): boolean {
        return !!this.categoryName;
    }
}
