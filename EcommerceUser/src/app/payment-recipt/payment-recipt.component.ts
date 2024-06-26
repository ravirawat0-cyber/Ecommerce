import {Component, ElementRef} from '@angular/core';
import {MatButton} from "@angular/material/button";
import {MatIcon} from "@angular/material/icon";
import {ActivatedRoute, RouterLink, RouterOutlet} from "@angular/router";
import {OrderService} from "../services/order.service";
import {MatSnackBar} from "@angular/material/snack-bar";


@Component({
  selector: 'app-payment-recipt',
  standalone: true,
  imports: [
    MatButton,
    MatIcon,
    RouterLink,
    RouterOutlet
  ],
  templateUrl: './payment-recipt.component.html',
  styleUrl: './payment-recipt.component.css'
})
export class PaymentReciptComponent {
  uuid: string = "";

  constructor(private route: ActivatedRoute, private orderService: OrderService, private snackbar: MatSnackBar) {
    this.route.params.subscribe(params => {
      this.uuid = params['uuid'];
    })
  }

  openRecipt() {
    this.orderService.getRecipt(this.uuid).subscribe(res => {
        window.open(res.data.receiptURL);
      },
      error => {
        this.snackbar.open(error.error, "Close", {duration: 3000});
      })
  }
}
