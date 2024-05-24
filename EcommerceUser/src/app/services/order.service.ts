import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {IHttp, IOrder, IOrderData} from "../models/order.model";

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  baseUrl = "http://ecommercerv.azurewebsites.net/Order"

  constructor(private http: HttpClient) {
  }

  getRecipt(uuid: string) {
    return this.http.get<IHttp<IOrder>>(`${this.baseUrl}/transactionId/${uuid}`);
  }

  getAllOrderDetail() {
    return this.http.get<IHttp<IOrderData>>(`${this.baseUrl}/all`);
  }
}
