import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ICategoryForm, ICategoryRes, IHttp } from '../models/category.model';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  baserUrl = 'http://ecommercerv.azurewebsites.net/category';

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
