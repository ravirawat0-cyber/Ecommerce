import {Component, OnInit} from '@angular/core';
import {MatIcon} from "@angular/material/icon";
import {MatIconButton} from "@angular/material/button";
import {FormsModule} from "@angular/forms";
import {Router} from "express";
import {ActivatedRoute} from "@angular/router";


@Component({
  selector: 'app-subcategory-products',
  standalone: true,
  imports: [
    MatIcon,
    MatIconButton,
    FormsModule
  ],
  templateUrl: './subcategory-products.component.html',
  styleUrl: './subcategory-products.component.css'
})
export class SubcategoryProductsComponent implements  OnInit{
   subCategoyrId!: number;
   subCategoryName : string = "";


   constructor(private activatedRoute: ActivatedRoute) {
   }

   ngOnInit(): void {
     this.activatedRoute.params.subscribe(params => {
       this.subCategoyrId = params['id'];
       this.subCategoryName = params['name'];
       console.log(this.subCategoyrId, this.subCategoryName);
     })
   }
}
