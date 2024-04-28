import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {MatGridList, MatGridTile} from "@angular/material/grid-list";

@Component({
  selector: 'app-subcategories-by-parent-container',
  standalone: true,
  imports: [
    MatGridList,
    MatGridTile
  ],
  templateUrl: './subcategories-by-parent-container.component.html',
  styleUrl: './subcategories-by-parent-container.component.css'
})
export class SubcategoriesByParentContainerComponent implements OnInit{
  categoryId! : number ;
  categoryName : string = "";

  constructor(private activatedRoute: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
      this.categoryId = params['id'];
      this.categoryName = params['name'];

    })
  }
}
