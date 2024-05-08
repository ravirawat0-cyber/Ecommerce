import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {IWishlistReq} from "../models/wishlist.model";


@Injectable({
  providedIn: 'root'
})
export class WishlistService {

  baseUrl = "https://localhost:7195/Wishlist"

  constructor(private http: HttpClient) { }

  AddToWishlist(Item: IWishlistReq)
  {
    return this.http.post(`${this.baseUrl}/add`, Item);
  }

  DeleteWislist(productId : number)
  {
    return this.http.delete(`${this.baseUrl}/${productId}`);
  }


}
