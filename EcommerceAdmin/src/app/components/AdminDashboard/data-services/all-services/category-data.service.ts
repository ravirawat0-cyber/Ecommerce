import { Injectable } from '@angular/core';
import {FormGroup} from "@angular/forms";
import {Observable, Subject} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class CategoryDataService {
    private categoryFormSubmittedSorce = new Subject<void>();
    categoryFormSubmitted$ : Observable<void > = this.categoryFormSubmittedSorce.asObservable();

  constructor() { }

    notifyCategoryFormSubmitted(): void {
      this.categoryFormSubmittedSorce.next();
    }
}
