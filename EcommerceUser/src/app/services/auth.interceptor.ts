import {Injectable} from "@angular/core";
import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    if (typeof localStorage != "undefined") {
      const storedToken = localStorage.getItem("user");

      if (storedToken != null) {
        const cloned = req.clone({headers: req.headers.set("Authorization", "Bearer " + storedToken)});
        return next.handle(cloned);
      }
    }
    return next.handle(req);
  }
}
