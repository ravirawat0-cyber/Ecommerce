import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, RouterLink, RouterLinkActive, RouterModule, RouterOutlet} from "@angular/router";
import {MatGridList, MatGridTile} from "@angular/material/grid-list";
import {SubCategoryService} from "../../services/sub-category.service";
import {ISubcategoryRes} from "../../models/subCategory.model";
import {NgForOf} from "@angular/common";

@Component({
  selector: 'app-subcategories-by-parent-container',
  standalone: true,
  imports: [
    MatGridList,
    MatGridTile,
    NgForOf,
    RouterLink,
    RouterOutlet,
    RouterModule,
    RouterLinkActive

  ],
  templateUrl: './subcategories-by-parent-container.component.html',
  styleUrl: './subcategories-by-parent-container.component.css'
})
export class SubcategoriesByParentContainerComponent implements OnInit{
  categoryId! : number ;
  categoryName : string = "";
  SubCategories : ISubcategoryRes[] = [];

  constructor(private activatedRoute: ActivatedRoute, private subCategoryService : SubCategoryService) {
  }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
      this.categoryId = params['id'];
      this.categoryName = params['name'];
      this.getCategories();

    })
  }

  getCategories(){
    this.subCategoryService.getByParentId(this.categoryId).subscribe(
      (response) => {
        this.SubCategories = response.data;
      }
    )
  }


}
