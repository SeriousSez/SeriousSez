// auth.guard.ts
import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { UserService } from '../services/user.service';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private userService: UserService, private router: Router) {}
  
  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot) { 
    if (this.userService.isAuthenticated()) {
      return true;
    }

    this.router.navigate(['/account/login'], { queryParams: { returnUrl: state.url }});
    return false;
  }  
}