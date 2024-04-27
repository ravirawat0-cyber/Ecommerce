import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ICategoryDataRes, IHttp} from "../models/category.model";

@Injectable({
  providedIn: 'root'
})
export class CategoryServicesService {

  baserUrl = 'https://localhost:7195/category';
  constructor(private https: HttpClient) { }

  getCategoryData()
  {
    return this.https.get<IHttp<ICategoryDataRes>>(`${this.baserUrl}/data`);
  }
}
