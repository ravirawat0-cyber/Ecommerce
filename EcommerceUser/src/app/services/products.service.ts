import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {IHttp, IProductProfileRes, IProductRes} from "../models/product.model";

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  baserUrl = 'https://localhost:7195/product';

  constructor(private https : HttpClient) { }

  getBySubCategoryId(subCategoryId: number)
  {
    return this.https.get<IHttp<IProductRes[]>>(`${this.baserUrl}/subcategory/${subCategoryId}`);
  }

  getByProductId(Id: number){
    return this.https.get<IHttp<IProductProfileRes>>(`${this.baserUrl}/${Id}`);
  }
}
