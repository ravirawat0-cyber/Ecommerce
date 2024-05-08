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
  userSubscription!: Subscription;
  loading = true;
  quantities: { [key: number]: number } = {};

  constructor(
    private accountService: AccountService,
    private cartService: CartService,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.loadUser();
  }

  ngOnDestroy(): void {
    this.userSubscription.unsubscribe();
  }

  loadUser(): void {
    this.userSubscription = this.accountService.user$.subscribe(user => {
      if (user) {
        this.userDetail = user;

        this.userDetail.cart.items.forEach(item => {
          this.quantities[item.productId] = item.quantity;
        });
        this.loading = false;
      }
    });
    this.accountService.loadUserFromToken().subscribe();
  }

  incrementQuantity(productId: number): void {
    this.quantities[productId]++;
  }

  decrementQuantity(productId: number): void {
    if (this.quantities[productId] > 1) {
      this.quantities[productId]--;
    }
  }

  updateCart(id: number): void {
    const req: ICartUpdateReq = {
      productId: id,
      quantity: this.quantities[id]
    };

    this.cartService.UpdateToCart(req).subscribe(
      () => {
        this.snackBar.open('Quantity Updated.', 'Close', { duration: 3000 });
        this.loadUser();
      },
      error => {
        this.snackBar.open('Error Occurred,', 'Close', { duration: 3000 });
      }
    );
  }

  deleteCart(id: number): void {
    this.cartService.DeleteToCart(id).subscribe(
      () => {
        this.snackBar.open('Product removed.', 'Close', { duration: 3000 });
        this.loadUser();
      },
      error => {
        this.snackBar.open('Error Occurred.', 'Close', { duration: 3000 });
      }
    );
  }

  clearCart(): void {
    this.cartService.DeleteCart().subscribe(
      () => {
        this.snackBar.open('Cart Cleared.', 'Close', { duration: 3000 });
        this.loadUser();
      },
      error => {
        this.snackBar.open('Error Occurred.', 'Close', { duration: 3000 });
      }
    );
  }
}
