import {Injectable} from '@angular/core';
import {IHttp, IUserLoginReq, IUserReq, IUserRes} from "../../app/models/user.model"
import {HttpClient} from "@angular/common/http";
import {BehaviorSubject, tap} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = "https://localhost:7195/Account"
  private userSubject = new BehaviorSubject<IUserRes | null>(null);
  user$ = this.userSubject.asObservable();

  constructor(private http: HttpClient) {
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
