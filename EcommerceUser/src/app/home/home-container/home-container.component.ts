import { Component } from '@angular/core';
import {HeaderComponent} from "../../global/header/header.component";
import {MatCard, MatCardContent} from "@angular/material/card";
import {CategoryContainerComponent} from "../category-container/category-container.component";

@Component({
  selector: 'app-home-container',
  standalone: true,
  imports: [
    HeaderComponent,
    MatCard,
    MatCardContent,
    CategoryContainerComponent
  ],
  templateUrl: './home-container.component.html',
  styleUrl: './home-container.component.css'
})
export class HomeContainerComponent {

}
