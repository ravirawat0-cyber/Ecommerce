import {Component, CUSTOM_ELEMENTS_SCHEMA} from '@angular/core';
import {MatIcon} from "@angular/material/icon";
import {MatButton} from "@angular/material/button";

@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [
    MatIcon,
    MatButton
  ],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.css',
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class ProductDetailsComponent {

}
