import { Injectable } from '@angular/core';
import {Observable, Subject} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class SubcategoryDataService {
    private subCategoryFormSubmittedSorce = new Subject<void>();
    subCategorySubmitted$: Observable<void > = this.subCategoryFormSubmittedSorce.asObservable();
  constructor() { }

    notifySubcategorySubmitted(): void {
      this.subCategoryFormSubmittedSorce.next()
    }
}
