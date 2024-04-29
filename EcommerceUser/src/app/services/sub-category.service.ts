import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {IHttp, ISubcategoryRes} from "../models/subCategory.model";

@Injectable({
  providedIn: 'root'
})
export class SubCategoryService {

  baserUrl = 'https://localhost:7195/subcategory';

  constructor(private http: HttpClient) { }


  getByParentId(parentId: number)
  {
    return this.http.get<IHttp<ISubcategoryRes[]>>(`${this.baserUrl}/parentCategory/${parentId}`)
  }
}
