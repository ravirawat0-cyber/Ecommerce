import {Component, Input} from '@angular/core';
import {CommonModule} from "@angular/common";

@Component({
  selector: 'app-category-container',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './category-container.component.html',
  styleUrl: './category-container.component.css'
})
export class CategoryContainerComponent {
  @Input() category: any;

  ngOnInit(): void {
    console.log(this.category);
  }
}
