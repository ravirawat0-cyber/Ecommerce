import {Injectable} from '@angular/core';
import {IHttp, IUserLoginReq, IUserReq, IUserRes, IUserUpdateReq} from "../../app/models/user.model"
import {HttpClient} from "@angular/common/http";
import {BehaviorSubject, catchError, map, Observable, of, tap, throwError} from "rxjs";
import {MatSnackBar} from "@angular/material/snack-bar";
import {IPurchaseRes} from "../models/cart.model";

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = "http://ecommercerv.azurewebsites.net/Account"
  private userSubject = new BehaviorSubject<IUserRes | null>(null);
  user$ = this.userSubject.asObservable();

  constructor(private http: HttpClient, private snakbar: MatSnackBar) {
  }

  loadUserFromToken() {
    return this.http.get<IHttp<IUserRes>>(`${this.baseUrl}`
    ).pipe(
      tap(response => {
        this.userSubject.next(response.data);
        localStorage.setItem('user', response.data.token.jwt);
      }),
      catchError(error => {
        return of(null);
      })
    );
  }


  login(user: IUserLoginReq) {
    return this.http.post<IHttp<IUserRes>>(`${this.baseUrl}/login`, user)
      .pipe(
        tap((response) => {
          this.userSubject.next(response.data);
          localStorage.setItem('user', response.data.token.jwt)
        })
      )
  }

  logout(): void {
    this.userSubject.next(null);
    localStorage.removeItem('user');
    this.snakbar.open("User Logout", "Close", {
      duration: 2000
    })
  }

  register(user: IUserReq) {
    return this.http.post<IHttp<IUserRes>>(`${this.baseUrl}/register`, user)
      .pipe(
        tap((response) => {
          this.userSubject.next(response.data);
          localStorage.setItem('user', response.data.token.jwt);
        })
      )
  }

  updateUser(user: IUserUpdateReq) {
    return this.http.put(`${this.baseUrl}/update`, user);
  }

  CartPurchase(UUID: string) {
    return this.http.get<IPurchaseRes>(`${this.baseUrl}/purchase/${UUID}`);
  }
}
