import {Component, OnInit} from '@angular/core';
import {MatButton} from "@angular/material/button";
import {MatIcon} from "@angular/material/icon";
import {MatCard} from "@angular/material/card";
import {IOrderData} from "../models/order.model";
import {MatSnackBar} from "@angular/material/snack-bar";
import {OrderService} from "../services/order.service";
import {DatePipe, NgForOf} from "@angular/common";
import {StarRatingModule} from "angular-star-rating";
import {MatDialog, MatDialogActions} from "@angular/material/dialog";
import {ReviewModalComponent} from "./review-modal/review-modal.component";

@Component({
  selector: 'app-orders',
  standalone: true,
  imports: [
    MatButton,
    MatIcon,
    MatCard,
    NgForOf,
    DatePipe,
    StarRatingModule,
    MatDialogActions
  ],
  templateUrl: './orders.component.html',
  styleUrl: './orders.component.css'
})
export class OrdersComponent implements OnInit{
  userOrders : IOrderData = { orderDetails: [] }

  constructor(private snackBar: MatSnackBar,
              private orderService : OrderService,
              private matDialog: MatDialog) {
  }

  ngOnInit(): void {
        this.getAllOrders();
    }

  getAllOrders()
  {
    this.orderService.getAllOrderDetail().subscribe(
      res => {
        console.log(res.data);
        this.userOrders = res.data;
      }
    )
  }

  writeReview(productId: number) {
     this.matDialog.open(ReviewModalComponent, {
       data : {productId}
     });
  }
}
