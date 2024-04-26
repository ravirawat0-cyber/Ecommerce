import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ICategoryForm, ICategoryRes, IHttp } from '../models/category.model';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  baserUrl = 'https://localhost:7195/category';

  constructor(private http: HttpClient) {}

  addCategory(data: ICategoryForm) {
    return this.http.post<Number>(`${this.baserUrl}/add`, data);
  }

  getCategory() {
    return this.http.get<IHttp<ICategoryRes[]>>(`${this.baserUrl}`);
  }

  deleteCategory(id: number) {
    return this.http.delete(`${this.baserUrl}/${id}`);
  }
}
