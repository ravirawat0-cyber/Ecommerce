import {Component, OnDestroy, OnInit} from '@angular/core';
import {MatButton} from "@angular/material/button";
import {MatIcon} from "@angular/material/icon";
import {IUserRes} from "../models/user.model";
import {Subscription} from "rxjs";
import {AccountService} from "../services/account.service";
import {WishlistService} from "../services/wishlist.service";
import {NgForOf, NgIf} from "@angular/common";
import {LoaderComponent} from "../global/loader/loader.component";
import {CartService} from "../services/cart.service";
import {ICartReq} from "../models/cart.model";
import {MatSnackBar} from "@angular/material/snack-bar";
import {MatCard} from "@angular/material/card";
import {LoginRequiredComponent} from "../global/login-required/login-required.component";

@Component({
  selector: 'app-wishlist',
  standalone: true,
  imports: [
    MatButton,
    MatIcon,
    NgForOf,
    LoaderComponent,
    NgIf,
    MatCard,
    LoginRequiredComponent,
  ],
  templateUrl: './wishlist.component.html',
  styleUrl: './wishlist.component.css'
})
export class WishlistComponent implements OnInit, OnDestroy {
  userDetail!: IUserRes ;
  isLoggedoff = true;
  userSubscription!: Subscription;
  loading: boolean = true;

  constructor(private accountService: AccountService,
              private wishlistService: WishlistService,
              private cartServices: CartService,
              private snackbar: MatSnackBar) {
  }

  ngOnInit(): void {
    this.loadUser();
  }

  loadUser() {
    this.userSubscription = this.accountService.user$.subscribe(user => {
      if (user) {
        this.userDetail = user;
        this.loading = false;
        this.isLoggedoff = false;
      }
    });
    this.accountService.loadUserFromToken().subscribe();
  }

  ngOnDestroy(): void {
    this.userSubscription.unsubscribe();
  }


  moveToCart(id: number): void {
    const item: ICartReq = {
      productId: id,
    }
    this.cartServices.AddToCart(item).subscribe(
      response => {
        if (response) {
          this.removeProduct(item.productId);
          this.loadUser();
          this.snackbar.open("Added to cart.", "Close",
            {duration: 3000});

        }
      },
      error => {
        this.snackbar.open(error.error, 'Close',
          {duration: 3000});

      }
    )
  }

  removeProduct(id: number) {
    this.wishlistService.DeleteWislist(id).subscribe(
      response => {
        this.snackbar.open('Product removed.', 'Close',
          {duration: 3000});
        this.loadUser();
      },
      error => {
        this.snackbar.open("Error occurred.", 'Close',
          {duration: 3000});
      }
    )
  }
}
