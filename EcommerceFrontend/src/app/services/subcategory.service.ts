import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IHttp, ISubcategoryForm } from '../models/subcategory.model';

@Injectable({
  providedIn: 'root',
})
export class SubcategoryService {
  baserUrl = 'https://localhost:7195/subcategory';
  constructor(private http: HttpClient) {}

  getSubcategory() {
    return this.http.get<IHttp<ISubcategoryForm>>(`${this.baserUrl}/get`);
  }

  addSubcategory(data: ISubcategoryForm) {
    return this.http.post<Number>(`${this.baserUrl}/add`, data);
  }

  deleteSubcategory(id: number) {
    this.http.delete(`${this.baserUrl}/delete/${id}`);
  }
}
