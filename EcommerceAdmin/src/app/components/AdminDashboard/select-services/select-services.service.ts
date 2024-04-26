import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SelectServicesService {
  private selectedServiceSubject = new BehaviorSubject<string>('');

  constructor() {}

  setSelectedService(service: string): void {
    this.selectedServiceSubject.next(service);
  }

  getSelectedService(): Observable<string> {
    return this.selectedServiceSubject.asObservable();
  }
}
