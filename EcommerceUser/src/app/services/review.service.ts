import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {IReviewReq} from "../models/review.model";

@Injectable({
  providedIn: 'root'
})
export class ReviewService {

  baserUrl = 'https://localhost:7195/Review';

  constructor(private http: HttpClient) { }

  addReview(data : IReviewReq){
    return this.http.post(`${this.baserUrl}/add`, data);
  }
}
