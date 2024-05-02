import { Injectable } from '@angular/core';
import {Subject} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class SidebarService {
  private isOpenSubject = new Subject<boolean>();
  isOpen$ = this.isOpenSubject.asObservable();

  toogleSidebar(){
    this.isOpenSubject.next(true);
  }
}
