import {Component, OnInit} from '@angular/core';
import {MatIcon} from "@angular/material/icon";
import {MatIconButton} from "@angular/material/button";
import {FormsModule} from "@angular/forms";
import {Router} from "express";
import {ActivatedRoute} from "@angular/router";
import {ProductsService} from "../services/products.service";
import {IProductRes} from "../models/product.model";
import {NgForOf} from "@angular/common";


@Component({
  selector: 'app-subcategory-products',
  standalone: true,
  imports: [
    MatIcon,
    MatIconButton,
    FormsModule,
    NgForOf
  ],
  templateUrl: './subcategory-products.component.html',
  styleUrl: './subcategory-products.component.css'
})
export class SubcategoryProductsComponent implements  OnInit{
   subCategoyrId!: number;
   subCategoryName : string = "";
   productDetails : IProductRes[] = [];

   constructor(private activatedRoute: ActivatedRoute, private productService : ProductsService) {
   }

   ngOnInit(): void {
     this.activatedRoute.params.subscribe(params => {
       this.subCategoyrId = params['id'];
       this.subCategoryName = params['name'];
     })

     this.fetchProductDetails();
   }

  fetchProductDetails() {
        this.productService.getBySubCategoryId(this.subCategoyrId).subscribe(
          (response) => {
            this.productDetails = response.products;
            console.log(this.productDetails);
          },
          error => {
            console.log(error.error);
          }
        )
    }

    splitKeyFeature(keyFeature:string): string[] {
     return keyFeature.split('\\n');
    }
}
