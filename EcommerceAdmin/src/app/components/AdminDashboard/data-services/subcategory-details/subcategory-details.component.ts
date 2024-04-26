import {Component} from '@angular/core';
import {Subscription} from "rxjs";
import {SubcategoryDataService} from "../all-services/subcategory-data.service";
import {response} from "express";
import {SubcategoryService} from "../../../../services/subcategory.service";
import {ISubcategoryRes} from "../../../../models/subcategory.model";

@Component({
    selector: 'app-subcategory-details',
    templateUrl: './subcategory-details.component.html',
    styleUrl: './subcategory-details.component.scss'
})
export class SubcategoryDetailsComponent {
    subCategories: ISubcategoryRes[] = [];
    private subCategoryFormSubmittedSubscription!: Subscription;

    constructor(private subCategoryService: SubcategoryService,
                private subCategoryDataService: SubcategoryDataService) {
    }

    ngOnInit(): void {
        this.fetchSubCategoryDetails()
        this.subCategoryFormSubmittedSubscription = this.subCategoryDataService.subCategorySubmitted$.subscribe(
            () => {
                this.fetchSubCategoryDetails();
            }
        )
    }

    ngOnDestroy(): void {
        this.subCategoryFormSubmittedSubscription.unsubscribe();
    }


    fetchSubCategoryDetails() {
        this.subCategoryService.getSubcategory().subscribe(
            (response) => {
                this.subCategories = response.data;
                console.log(this.subCategories);
            },
            (error) => {
                console.log("Error fetching SubcategoryDetails:", error);
            }
        )
    }

    deleteSubCategory(id: number) {
        this.subCategoryService.deleteSubcategory(id).subscribe(
            () => {
                this.fetchSubCategoryDetails();
            },
            (error: any) => {
                console.log(`Error deleting with Id ${id}`,error);
            }
         )
    }

}
