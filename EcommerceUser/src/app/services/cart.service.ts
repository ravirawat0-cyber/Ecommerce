import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ICartReq} from "../models/cart.model";

@Injectable({
  providedIn: 'root'
})
export class CartService {

  baseUrl = "http://ecommercerv.azurewebsites.net/Cart"

  constructor(private http: HttpClient) {
  }


  AddToCart(Item: ICartReq) {
    return this.http.post(`${this.baseUrl}/add`, Item);
  }

  UpdateToCart(Item: ICartReq) {
    return this.http.put(`${this.baseUrl}`, Item);
  }

  DeleteToCart(prouductId: number) {
    return this.http.delete(`${this.baseUrl}/${prouductId}`);
  }

  DeleteCart() {
    return this.http.delete(`${this.baseUrl}/deletecart`)
  }

}

