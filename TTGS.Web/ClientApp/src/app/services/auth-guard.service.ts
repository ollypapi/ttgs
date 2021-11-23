import { Router } from '@angular/router';
// import { AuthService } from './auth.service';
// import { Injectable } from '@angular/core';
// import { CanActivate } from '@angular/router';

// @Injectable({
//   providedIn: 'root'
// })
// export class AuthGuardService implements CanActivate {

//   constructor(private auth : AuthService) { }

//   canActivate(): boolean {
//     if (!this.auth.isAuthenticated()) {
//       // this.router.navigate(['app-dashboard']);
//       return false;
//     }
//     return true;
//   }
// }

import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';

import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

  constructor(private auth: AuthService, private router : Router) {}
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      if (this.auth.isLoggedIn()) {
        return true;
      }
      window.alert('You don\'t have permission to view this page');
      this.router.navigate(['login']);
      return false;
  }
}
