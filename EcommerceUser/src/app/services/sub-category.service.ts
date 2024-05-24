import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {IHttp, ISubcategoryRes} from "../models/subCategory.model";

@Injectable({
  providedIn: 'root'
})
export class SubCategoryService {

  baserUrl = 'http://ecommercerv.azurewebsites.net/subcategory';

  constructor(private http: HttpClient) {
  }


  getByParentId(parentId: number) {
    return this.http.get<IHttp<ISubcategoryRes[]>>(`${this.baserUrl}/parentCategory/${parentId}`)
  }
}
