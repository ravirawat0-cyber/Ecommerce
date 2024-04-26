import { Injectable } from '@angular/core';
import {Observable, Subject} from "rxjs";


@Injectable({
  providedIn: 'root'
})
export class ProductDataService {
  private productFromSubmittedSorce = new Subject<void>();
  productFormSubmitted$ : Observable<void> = this.productFromSubmittedSorce.asObservable();
  constructor() { }

    notifyProductFormSubmitted(): void {
      this.productFromSubmittedSorce.next();
    }
}
