import { Component, Input } from '@angular/core';
import { CategoryformComponent } from '../categoryform/categoryform.component';
import { SubcategoryformComponent } from '../subcategoryform/subcategoryform.component';
import { ProductFormComponent } from '../../products/product-form/product-form.component';

@Component({
  selector: 'app-data-dashboard',
  templateUrl: './data-dashboard.component.html',
  styleUrl: './data-dashboard.component.scss',
})
export class DataDashboardComponent {
  @Input() selectedService: string = '';
}
