import {Component} from '@angular/core';
import {SelectServicesService} from '../../select-services/select-services.service';

@Component({
  selector: 'app-data-dashboard',
  templateUrl: './data-dashboard.component.html',
  styleUrl: './data-dashboard.component.scss',
})
export class DataDashboardComponent {
  selectedService: string = '';

  constructor(private selectedServiceService: SelectServicesService) {
  }

  ngOnInit(): void {
    this.selectedServiceService.getSelectedService().subscribe((service) => {
      this.selectedService = service;
      console.log(this.selectedService);

    });
  }
}
