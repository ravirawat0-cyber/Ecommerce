import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SelectServicesService } from './select-services.service';
@Component({
  selector: 'app-select-services',
  templateUrl: './select-services.component.html',
  styleUrl: './select-services.component.scss',
})
export class SelectServicesComponent {
  selectedService: string = '';

  constructor(private selectedServiceService: SelectServicesService) {}

  onServiceSelectionChange() {
    this.selectedServiceService.setSelectedService(this.selectedService);
  }
}
