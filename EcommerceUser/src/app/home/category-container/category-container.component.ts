import {Component, Input} from '@angular/core';
import {CommonModule} from "@angular/common";
import {RouterLink, RouterOutlet} from "@angular/router";

@Component({
  selector: 'app-category-container',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterOutlet],
  templateUrl: './category-container.component.html',
  styleUrl: './category-container.component.css'
})
export class CategoryContainerComponent {
  @Input() category: any;

  ngOnInit(): void {
  }
}
