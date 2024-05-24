import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ImageResponse} from "../models/image.model";

@Injectable({
  providedIn: 'root'
})
export class ImageService {
  baseUrl = "http://ecommercerv.azurewebsites.net/Upload"

  constructor(private http: HttpClient) {
  }

  UploadImage(image: FormData) {
    return this.http.post<ImageResponse>(this.baseUrl, image)
  }

}
