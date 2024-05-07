import {Component, OnDestroy, OnInit} from '@angular/core';
import {MatCard, MatCardContent} from "@angular/material/card";
import {MatIcon} from "@angular/material/icon";
import {MatDivider} from "@angular/material/divider";
import {IUserRes} from "../models/user.model";
import {AccountService} from "../services/account.service";
import {Subscription} from "rxjs";
import { ICartUpdateReq} from "../models/cart.model";
import {NgForOf, NgIf} from "@angular/common";
import {MatProgressBar} from "@angular/material/progress-bar";
import {MatProgressSpinner} from "@angular/material/progress-spinner";
import {LoaderComponent} from "../global/loader/loader.component";
import {CartService} from "../services/cart.service";
import {MatSnackBar} from "@angular/material/snack-bar";
import {user} from "@angular/fire/auth";

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [
    MatCardContent,
    MatCard,
    MatIcon,
    MatDivider,
    NgForOf,
    NgIf,
    MatProgressBar,
    MatProgressSpinner,
    LoaderComponent
  ],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent implements OnInit, OnDestroy {
  userDetail!: IUserRes;
  userSubcription!: Subscription;
  loading = true;
  quantity = 1

  constructor(private accountService: AccountService, private cartService: CartService, private snackBar: MatSnackBar) {
  }


  ngOnInit(): void {
   this.loadUser();
  }

  ngOnDestroy(): void {
    this.userSubcription.unsubscribe();
  }

  loadUser(){
    this.userSubcription = this.accountService.user$.subscribe(user => {
      if (user) {
        this.userDetail = user;
        console.log("cart", this.userDetail);
        this.loading = false;
      }
    });
    this.accountService.loadUserFromToken().subscribe();
  }

  incrementQuantity(): void {
    this.quantity++;
  }

  decrementQuantity(): void {
    if (this.quantity > 1) {
      this.quantity--;
    }
  }

  updateCart(id: number) {
    const req: ICartUpdateReq = {
      productId: id,
      quantity: this.quantity
    }

    this.cartService.UpdateToCart(req).subscribe(
      (response) => {
            this.snackBar.open("Quantity Updated.", 'Close', {duration: 3000});
            this.loadUser();
      },
      error => {
         this.snackBar.open("Error Occured,", "Close", {duration: 3000});
      }
    )
  }

  deleteCart(id: number) {
    this.cartService.DeleteToCart(id).subscribe(
      (response) => {
        this.snackBar.open("Product deleted.", 'Close', {duration: 3000});
        this.loadUser();
      },
      error => {
        this.snackBar.open("Error Occured.", "Close", {duration: 3000});
      }
    )
  }

  protected readonly user = user;
}
