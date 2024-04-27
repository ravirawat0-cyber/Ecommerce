import { Component } from '@angular/core';
import {NavbarComponent} from '../navbar/navbar.component'
import {CategoryNavComponent} from "../category-nav/category-nav.component";

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [NavbarComponent, CategoryNavComponent],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {

}
