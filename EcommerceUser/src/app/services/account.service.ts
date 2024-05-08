import {Injectable} from '@angular/core';
import {IHttp, IUserLoginReq, IUserReq, IUserRes} from "../../app/models/user.model"
import {HttpClient} from "@angular/common/http";
import {BehaviorSubject, catchError, Observable, of, tap} from "rxjs";
import {J} from "@angular/cdk/keycodes";
import {user} from "@angular/fire/auth";
import {MatSnackBar} from "@angular/material/snack-bar";

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = "https://localhost:7195/Account"
  private userSubject = new BehaviorSubject<IUserRes | null>(null);
  user$ = this.userSubject.asObservable();

  constructor(private http: HttpClient, private snakbar : MatSnackBar) {

  }

  loadUserFromToken()  {
    return this.http.get<IHttp<IUserRes>>(`${this.baseUrl}`
       ).pipe(
         tap(response => {
           this.userSubject.next(response.data);
           localStorage.setItem('user', response.data.token.jwt);
           console.log(response.data);
         }),
         catchError(error => {
           console.log(error.error);
           return of(null);
         })
       );
    }


  login(user : IUserLoginReq) {
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
}
