import {Component, OnInit} from '@angular/core';
import {MatCard, MatCardContent} from "@angular/material/card";
import {CategoryContainerComponent} from "../category-container/category-container.component";
import {CategoryServicesService} from "../../services/category-services.service";
import {CommonModule} from "@angular/common";
import {ICategoryDataRes} from "../../models/category.model";
import {LoaderComponent} from "../../global/loader/loader.component";


@Component({
  selector: 'app-home-container',
  standalone: true,
  imports: [
    MatCard,
    MatCardContent,
    CategoryContainerComponent,
    CommonModule,
    LoaderComponent,
    MatCardContent
  ],
  templateUrl: './home-container.component.html',
  styleUrl: './home-container.component.css'
})
export class HomeContainerComponent implements OnInit {

  categorySubcategoryData : ICategoryDataRes[] = [];
  isLoading : boolean = true;

  constructor(private categoryService: CategoryServicesService ) {}

   ngOnInit()
  {
    setTimeout(() => {this.fetchCategoryData()}, 1000)

  }

 fetchCategoryData() {
        this.categoryService.getCategoryData().subscribe((response) => {
          this.categorySubcategoryData = response.data;
          this.isLoading = false;
        },
          error => {
             console.log(error.error);
        })
    }
}
