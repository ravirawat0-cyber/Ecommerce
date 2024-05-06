import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ICartReq} from "../models/cart.model";

@Injectable({
  providedIn: 'root'
})
export class CartService {

  baseUrl = "https://localhost:7195/Cart"
  constructor(private http: HttpClient) { }


  AddToCard(Item: ICartReq)
  {
    return this.http.post(`${this.baseUrl}/add`, Item);
  }

  UpdateToCard(Item: ICartReq)
  {
    return this.http.put(`${this.baseUrl}`, Item);
  }
}

