import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-select-services',
  templateUrl: './select-services.component.html',
  styleUrl: './select-services.component.scss',
})
export class SelectServicesComponent {
  selectedService: string = '';
  showModal: string = '';

  openSelectedModal() {
    this.showModal = this.selectedService;
  }
  closeModal() {
    this.showModal = '';
  }
}
