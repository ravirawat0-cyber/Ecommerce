import {CanActivateFn, Router} from '@angular/router';
import {inject} from "@angular/core";
import {AccountService} from "../services/account.service";
import {map} from "rxjs";

export const authGuard: CanActivateFn = (route, state) => {
     const accountService = inject(AccountService);
     const router = inject(Router)

     return accountService.user$.pipe(
       map(user => {
         if (user){
           return true;
         }
         else {
           const userToken = localStorage.getItem('user');
           if (userToken) {
             accountService.loadUserFromToken().subscribe();
             return true;
           }
           else {
             return router.createUrlTree(['/login'])
           }
         }
       })
     )
};
