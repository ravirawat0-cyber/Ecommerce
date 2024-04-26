import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  IHttp,
  ISubcategoryForm,
  ISubcategoryRes,
} from '../models/subcategory.model';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SubcategoryService {
  baserUrl = 'https://localhost:7195/subcategory';


  constructor(private http: HttpClient) {}

  getSubcategory() {
    return this.http.get<IHttp<ISubcategoryRes[]>>(`${this.baserUrl}`);
  }

  addSubcategory(data: ISubcategoryForm) {
    return this.http.post<Number>(`${this.baserUrl}/add`, data);
  }

  deleteSubcategory(id: number) {
    return this.http.delete(`${this.baserUrl}/${id}`);
  }
}
