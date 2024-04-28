import {Component} from '@angular/core';
import {HeaderComponent} from "../../global/header/header.component";
import {MatCard, MatCardContent} from "@angular/material/card";
import {CategoryContainerComponent} from "../category-container/category-container.component";
import {CategoryServicesService} from "../../services/category-services.service";
import {CommonModule} from "@angular/common";
import {ICategoryDataRes} from "../../models/category.model";

@Component({
  selector: 'app-home-container',
  standalone: true,
  imports: [
    HeaderComponent,
    MatCard,
    MatCardContent,
    CategoryContainerComponent,
    CommonModule,
  ],
  templateUrl: './home-container.component.html',
  styleUrl: './home-container.component.css'
})
export class HomeContainerComponent {

  categorySubcategoryData : ICategoryDataRes[] = [];
  constructor(private categoryService: CategoryServicesService ) {
  }


  ngOnInit(): void
  {
    this.fetchCategoryData();
  }

  fetchCategoryData() {
        this.categoryService.getCategoryData().subscribe((response) => {
          this.categorySubcategoryData = response.data;
        })
    }



}
