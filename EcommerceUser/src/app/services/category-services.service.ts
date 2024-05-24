import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ICategoryDataRes, ICategoryRes, IHttp} from "../models/category.model";


@Injectable({
  providedIn: 'root'
})
export class CategoryServicesService {

  baserUrl = 'http://ecommercerv.azurewebsites.net/category';

  constructor(private https: HttpClient) {
  }

  getCategoryData() {
    return this.https.get<IHttp<ICategoryDataRes[]>>(`${this.baserUrl}/data`);
  }

  getCategory() {
    return this.https.get<IHttp<ICategoryRes[]>>(`${this.baserUrl}`);
  }
}
