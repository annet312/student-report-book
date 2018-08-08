import { Injectable, Inject } from '@angular/core';
import decode from 'jwt-decode';
import { tokenNotExpired } from 'angular2-jwt';
import { DecodeService } from '../shared/services/decode.service';
import { HttpClient } from '../../../node_modules/@angular/common/http';

@Injectable()
export class AuthService {

  baseUrls: string = '';

  constructor(private decodeService: DecodeService, private httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrls = baseUrl;
  }

  public getToken(): string {
    return localStorage.getItem('auth_token');   
  }

  public setToken(token) {
    localStorage.setItem('auth_token', token);
    return true;
  }

  public getCurrentUser() {
    let userName: string = null;
    if (this.isAuthenticated()) {
      let token = this.getToken();
      userName = this.decodeService.getDecodedAccessToken(token).sub;
    }
    return userName;
  }

  getCurrentUserRole() {
    let userRole: string = 'no role';
    if (this.isAuthenticated()) {
      if (localStorage.getItem('current_role') == null) {
        this.httpClient.get<string>(this.baseUrls + 'api/auth/getCurrentRole').subscribe(result => {
          localStorage.setItem('current_role', result);
          userRole = localStorage.getItem('current_role');
        }, error => console.error(error));
      }
      else {
         userRole = localStorage.getItem('current_role');
      }
    }
     return userRole;
  }

  public isAuthenticated(): boolean {
    // get the token
    const token = this.getToken();
    // return a boolean reflecting 
    // whether or not the token is expired
    return tokenNotExpired(null, token);
  }
}
