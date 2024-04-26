import { Component } from '@angular/core';
import {ICategoryRes} from "../../../../models/category.model";
import {CategoryService} from "../../../../services/category.service";
import {Subscription} from "rxjs";
import {CategoryDataService} from "../all-services/category-data.service";


@Component({
  selector: 'app-category-details',
  templateUrl: './category-details.component.html',
  styleUrl: './category-details.component.scss'
})
export class CategoryDetailsComponent {
    categories : ICategoryRes[]= []
    private cateogoryFormSubmittedSubscription!: Subscription;

    constructor(private categoryService: CategoryService,
                private categoryDataService: CategoryDataService) {
    }

    ngOnInit(): void {
        this.fetchCategoryDetails();
        this.cateogoryFormSubmittedSubscription = this.categoryDataService.categoryFormSubmitted$.subscribe( () => {
            this.fetchCategoryDetails();
        })
    }
    ngOnDestroy(): void {
        this.cateogoryFormSubmittedSubscription.unsubscribe();
    }

    fetchCategoryDetails(): void {
        this.categoryService.getCategory().subscribe(
            (response) => {
                this.categories = response.data;
                console.log(this.categories);
            },
            (error) => {
                console.log("Error fetching categories details:", error);
            }
        )
    }

    deleteCategory(id: number) {
        this.categoryService.deleteCategory(id).subscribe(
            () => {
                this.fetchCategoryDetails();
            },
            (error: any) => {
                console.log('Error deleting with Id ' + id);
            }
        )
    }
}
