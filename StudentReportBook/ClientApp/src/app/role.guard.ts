import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot } from '@angular/router';
import { AuthService } from './auth/auth.service';

@Injectable()
export class RoleGuardService implements CanActivate {

  constructor(public auth: AuthService, public router: Router) { }

  canActivate(route: ActivatedRouteSnapshot): boolean {

    // this will be passed from the route config
    // on the data property
    const expectedRole = route.data.expectedRole;
    console.log(expectedRole);
    const tokenRole = this.auth.getCurrentUserRole();
    console.log(tokenRole)
    if (!this.auth.isAuthenticated() || tokenRole !== expectedRole) {
      this.router.navigate(['']);
      return false;
    }
    return true;
  }

}
