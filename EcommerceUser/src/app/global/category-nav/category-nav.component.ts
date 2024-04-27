import { Component } from '@angular/core';
import {MatToolbar} from "@angular/material/toolbar";
import {MatDivider} from "@angular/material/divider";

@Component({
  selector: 'app-category-nav',
  standalone: true,
  imports: [
    MatToolbar,
    MatDivider
  ],
  templateUrl: './category-nav.component.html',
  styleUrl: './category-nav.component.css'
})
export class CategoryNavComponent {

}
