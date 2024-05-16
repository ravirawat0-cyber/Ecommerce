import {Component, CUSTOM_ELEMENTS_SCHEMA, OnInit} from '@angular/core';
import {MatIcon} from "@angular/material/icon";
import {MatButton} from "@angular/material/button";
import {IProductProfileRes} from "../models/product.model";
import {ProductsService} from "../services/products.service";
import {NgForOf, NgIf} from "@angular/common";
import {ActivatedRoute, RouterLink, RouterOutlet} from "@angular/router";
import {CartService} from "../services/cart.service";
import {ICartReq} from "../models/cart.model";
import {AccountService} from "../services/account.service";
import {MatSnackBar} from "@angular/material/snack-bar";
import {StarRatingComponent} from "../global/star-rating/star-rating.component";
import {MatCard} from "@angular/material/card";
import {IReviewResponse} from "../models/review.model";
import {ReviewService} from "../services/review.service";
import jwt_decode, {jwtDecode} from 'jwt-decode';
import {MatDialog} from "@angular/material/dialog";
import {ReviewUpdateModelComponent} from "./review-update-model/review-update-model.component";

@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [
    MatIcon,
    MatButton,
    NgForOf,
    RouterLink,
    RouterOutlet,
    StarRatingComponent,
    MatCard,
    NgIf
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
  productReview!: IReviewResponse;
  userId! : string;

  starStyle = {
    'margin': '0',
    'alignItems' : 'center',
    'background': '#e8e8e8',
      'borderRadius': '4px',
  };

  childStyle  = {
    fontSize: '20px',
}
  buttonSyle = {
    'cursor' : 'default'
  }


  constructor(private accountService : AccountService,
              private snackbar : MatSnackBar,
              private productService :ProductsService,
              private cartServices : CartService,
              private activatedRoute : ActivatedRoute,
              private reviewService: ReviewService,
              private matDialog: MatDialog) {
  }

  ngOnInit(){
    this.activatedRoute.params.subscribe(params => {
      this.productId = params['id'];
    })
    this.fetchProductDetail();
    this.getReview();
    this.extractUserIdFromToken();
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

  getReview(){
    this.reviewService.GetReviewById(this.productId).subscribe(
      res => {
        this.productReview = res;
      }
    )
  }

  extractUserIdFromToken(){
    const token = localStorage.getItem("user");
    if(token){
      const decodedToken: any = jwtDecode(token);
      this.userId = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata'];
    }
  }

  isReviewOwner(reviewUserId: number):boolean {
    return reviewUserId === parseInt(this.userId);
  }

  updateReview(productId : number){
    const dialogRef = this.matDialog.open(ReviewUpdateModelComponent, {
      data: {productId}
    });
    dialogRef.afterClosed().subscribe(result => {
        this.getReview();
    });

  }

  deleteReview(productId: number) {
        this.reviewService.DeleteReview(productId).subscribe(
          res => {
            this.snackbar.open("Review Deleted", 'Close', { duration: 3000});
            this.getReview();
          }
        )
  }
}
