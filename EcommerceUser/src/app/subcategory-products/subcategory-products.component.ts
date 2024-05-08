import {Component, OnInit} from '@angular/core';
import {MatIcon} from "@angular/material/icon";
import {MatIconButton} from "@angular/material/button";
import {FormsModule} from "@angular/forms";
import {ActivatedRoute, RouterLink, RouterOutlet} from "@angular/router";
import {ProductsService} from "../services/products.service";
import {IProductRes} from "../models/product.model";
import {NgForOf} from "@angular/common";
import {AccountService} from "../services/account.service";
import {CartService} from "../services/cart.service";
import {WishlistService} from "../services/wishlist.service";
import {ICartReq} from "../models/cart.model";
import {MatSnackBar} from "@angular/material/snack-bar";
import {IWishlistReq} from "../models/wishlist.model";


@Component({
  selector: 'app-subcategory-products',
  standalone: true,
  imports: [
    MatIcon,
    MatIconButton,
    FormsModule,
    NgForOf,
    RouterLink,
    RouterOutlet
  ],
  templateUrl: './subcategory-products.component.html',
  styleUrl: './subcategory-products.component.css'
})
export class SubcategoryProductsComponent implements OnInit {
  subCategoyrId!: number;
  subCategoryName: string = "";
  productDetails: IProductRes[] = [];

  constructor(private activatedRoute: ActivatedRoute,
              private productService: ProductsService,
              private accountService: AccountService,
              private cartServices: CartService,
              private wishlistServices: WishlistService,
              private snackbar: MatSnackBar) {
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

  splitKeyFeature(keyFeature: string): string[] {
    return keyFeature.split('\\n');
  }


  addToCart(id: number) {
    const item: ICartReq = {
      productId: id,
    }
    this.cartServices.AddToCart(item).subscribe(
      (response) => {
        if (response) {
          this.accountService.loadUserFromToken().subscribe();
          this.snackbar.open("Added to cart.", "Close",
            {duration: 3000}
          )
        }
      },
      error => {
        this.snackbar.open(error.error, 'Close',
          {duration: 3000});
      }
    )
  }

  addToWishlist(id: number) {
    const item: IWishlistReq = {
      productId: id,
    }
    this.wishlistServices.AddToWishlist(item).subscribe(
      (response) => {
        if (response) {
          this.accountService.loadUserFromToken().subscribe();
          this.snackbar.open("Added to wishlist.", "Close",
            {duration: 3000})
        }
      },
      error => {
        this.snackbar.open(error.error, 'Close',
          {duration: 3000});
      }
    )
  }
}
