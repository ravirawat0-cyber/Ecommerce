import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {IReviewReq, IReviewResponse} from "../models/review.model";

@Injectable({
  providedIn: 'root'
})
export class ReviewService {

  baserUrl = 'http://ecommercerv.azurewebsites.net/Review';

  constructor(private http: HttpClient) {
  }

  addReview(data: IReviewReq) {
    return this.http.post(`${this.baserUrl}/add`, data);
  }

  GetReviewById(productId: number) {
    return this.http.get<IReviewResponse>(`${this.baserUrl}/product/${productId}`);
  }

  UpdateReview(data: IReviewReq) {
    return this.http.put(`${this.baserUrl}/update`, data);
  }

  DeleteReview(productId: number) {
    return this.http.delete(`${this.baserUrl}/product/${productId}`);
  }
}
