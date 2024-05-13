import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {IHttp, IOrderRes} from "../models/Order.model";

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  baseUrl = "https://localhost:7195/Order"
  constructor(private http : HttpClient) { }

  getRecipt(uuid: string){
    return this.http.get<IHttp<IOrderRes[]>>(`${this.baseUrl}/transactionId/${uuid}`);
  }

}
