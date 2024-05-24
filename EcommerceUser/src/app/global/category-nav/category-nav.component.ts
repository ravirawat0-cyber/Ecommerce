import {Component} from '@angular/core';
import {MatToolbar} from "@angular/material/toolbar";
import {MatDivider} from "@angular/material/divider";
import {RouterLink, RouterLinkActive, RouterOutlet} from "@angular/router";
import {CategoryServicesService} from "../../services/category-services.service";
import {ICategoryRes} from "../../models/category.model";
import {CommonModule} from "@angular/common";
import {SidenavComponent} from "../sidenav/sidenav.component";
import {MatCard} from "@angular/material/card";
import {MatMenu} from "@angular/material/menu";

@Component({
  selector: 'app-category-nav',
  standalone: true,
  imports: [
    MatToolbar,
    MatDivider,
    RouterOutlet,
    RouterLinkActive,
    RouterLink,
    CommonModule,
    SidenavComponent,
    MatCard,
    MatMenu
  ],
  templateUrl: './category-nav.component.html',
  styleUrl: './category-nav.component.css'
})
export class CategoryNavComponent {

  categories: ICategoryRes[] = []

  constructor(private categoryService: CategoryServicesService) {
  }

  ngOnInit(): void {
    this.fetchDetails();
  }

  fetchDetails() {
    this.categoryService.getCategory().subscribe((response) => {
      this.categories = response.data;
    });
  }
}
