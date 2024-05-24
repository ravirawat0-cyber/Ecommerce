import {
  ActivatedRouteSnapshot,
  CanActivate,
  CanActivateFn,
  Router,
  RouterStateSnapshot,
  UrlTree
} from '@angular/router';
import {Injectable} from "@angular/core";
import {map, Observable} from "rxjs";
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})

export class AuthGuard implements CanActivate {

  constructor(private accountService: AccountService, private router: Router) {
  }

  canActivate(route: ActivatedRouteSnapshot,
              state: RouterStateSnapshot) : Observable<boolean | UrlTree > {
     return this.accountService.user$.pipe(
       map(user => {
         if (user)
         {
           return true
         }
         else {
           this.router.navigate(['/login']);
           return false;
         }
       })
     )
  }
}

