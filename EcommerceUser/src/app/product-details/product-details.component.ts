import {Component, CUSTOM_ELEMENTS_SCHEMA, OnInit} from '@angular/core';
import {MatIcon} from "@angular/material/icon";
import {MatButton} from "@angular/material/button";
import {IProductProfileRes} from "../models/product.model";
import {ProductsService} from "../services/products.service";
import {NgForOf} from "@angular/common";
import {ActivatedRoute, RouterLink, RouterOutlet} from "@angular/router";
import {CartService} from "../services/cart.service";
import {ICartReq} from "../models/cart.model";
import {AccountService} from "../services/account.service";
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [
    MatIcon,
    MatButton,
    NgForOf,
    RouterLink,
    RouterOutlet
  ],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.css',
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class ProductDetailsComponent implements OnInit {

  productDetail!: IProductProfileRes;
  productId! : number;
  imageUrls : string [] = [];
  keyFeatures : string[] = [];
  currentImageIndex = 0;

  constructor(private accountService : AccountService ,private snackbar : MatSnackBar, private productService :ProductsService ,private cartServices : CartService, private activatedRoute : ActivatedRoute) {
  }

  ngOnInit(){
    this.activatedRoute.params.subscribe(params => {
      this.productId = params['id'];
      console.log(this.productId);
    })
    this.fetchProductDetail();
  }

  fetchProductDetail() {
        this.productService.getByProductId(this.productId).subscribe(
          (response) => {
            this.productDetail = response.products;

            if (this.productDetail.imageUrls) {
              this.imageUrls = this.productDetail.imageUrls.split('|');
            }

            if (this.productDetail.keyFeature) {
              this.keyFeatures = this.productDetail.keyFeature.split('\\n');
            }


          },
          error => {
            console.log(error.error);
          }
        )
    }

  nextImage(){
    this.currentImageIndex = (this.currentImageIndex + 1)% this.imageUrls.length;
  }
  previousImage(){
    this.currentImageIndex = (this.currentImageIndex - 1 + this.imageUrls.length)% this.imageUrls.length;
  }

  addToCart() {
      const item : ICartReq ={
        productId: this.productId,
    }
      this.cartServices.AddToCart(item).subscribe(
         (response) => {
           if (response) {
             this.accountService.loadUserFromToken().subscribe();
             this.snackbar.open("Added to cart.", "Close",
               { duration: 3000}
             )}
         },
        error => {
           this.snackbar.open(error.error, 'Close',
             {duration: 3000});
        }
      )
  }
}
