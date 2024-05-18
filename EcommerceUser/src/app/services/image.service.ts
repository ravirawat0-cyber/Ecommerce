import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ImageResponse} from "../models/image.model";

@Injectable({
  providedIn: 'root'
})
export class ImageService {
  baseUrl = "https://localhost:7195/Upload"
  constructor(private http: HttpClient) {
  }
    UploadImage(image  : FormData)
    {
      return this.http.post<ImageResponse>(this.baseUrl, image)
    }

}
