import {Component, ElementRef} from '@angular/core';
import {DomSanitizer, SafeResourceUrl} from "@angular/platform-browser";
import {MatButton} from "@angular/material/button";
import {MatIcon} from "@angular/material/icon";

@Component({
  selector: 'app-payment-recipt',
  standalone: true,
  imports: [
    MatButton,
    MatIcon
  ],
  templateUrl: './payment-recipt.component.html',
  styleUrl: './payment-recipt.component.css'
})
export class PaymentReciptComponent {
  externalLink!: SafeResourceUrl ;
  constructor(private sanitizer: DomSanitizer) {
    this.externalLink = this.sanitizer.bypassSecurityTrustResourceUrl('https://th.bing.com/th/id/OIG3.5u5ZBGkvLQn1ELp4UqXH');
  }
}
