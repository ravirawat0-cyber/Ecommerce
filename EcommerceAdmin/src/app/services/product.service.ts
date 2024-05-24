import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {IHttp, IProductForm, IProductRes} from '../models/product.model';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  baseUrl = 'http://ecommercerv.azurewebsites.net/product';

  constructor(private http: HttpClient) {}

  addProduct(data: IProductForm) {
    return this.http.post<Number>(`${this.baseUrl}/add`, data);
  }

  getProduct() {
    return this.http.get<IHttp<IProductRes[]>>(`${this.baseUrl}`);
  }

  deleteProduct(Id: number)
  {
      return this.http.delete(`${this.baseUrl}/${Id}`);
  }
}
