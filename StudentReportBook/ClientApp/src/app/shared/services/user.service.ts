import { Injectable } from '@angular/core';
import { Inject } from '@angular/core';

import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { HttpClient } from '@angular/common/http';

import { UserRegistration } from '../models/user.registration.interface';
import { ConfigService } from '../utils/config.service';
import { UserResponse } from '../models/UserResponse';
import { DecodeService } from './decode.service';
import { BaseService } from "./base.service";



import { Observable } from 'rxjs/Rx';
import { BehaviorSubject } from 'rxjs/Rx';

//import { UserResponse } from '../models/UserResponse';

// Add the RxJS Observable operators we need in this app.
import '../../rxjs-operators';

@Injectable()

export class UserService extends BaseService {

  baseUrls: string = '';

  //Observable navItem source
  private authNavStatusSource = new BehaviorSubject<boolean>(false);
  //Observable navItem stream
  authNavStatus$ = this.authNavStatusSource.asObservable();

  private loggedIn = false;

  constructor(private http: Http, private configService: ConfigService, @Inject('BASE_URL') baseUrl: string, private decodeService: DecodeService) {
    super();
    this.loggedIn = !!localStorage.getItem('auth_token');
    this.authNavStatusSource.next(this.loggedIn);
    this.baseUrls = baseUrl;
    //this.baseUrl = configService.getApiURI();
  }

  register(email: string, password: string, firstName: string, lastName: string): Observable<UserRegistration> {
    let body = JSON.stringify({ email, password, firstName, lastName });
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });

    return this.http.post(this.baseUrls + "api/accounts", body, options)
      .map(res => true)
      .catch(this.handleError);
  }  

  login(userName: string, password : string) {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');

    return this.http
      .post(
        this.baseUrls + 'api/auth/login',
        JSON.stringify({ userName, password }), { headers }
      )
      .map(res => res.json())
      .map(res => {
        localStorage.setItem('auth_token', res.auth_token);
        this.loggedIn = true;
        this.authNavStatusSource.next(true);
        return true;
      })
      .catch(this.handleError);
  }

  getCurrentUser() {
    let userName: string = null;
    if (this.loggedIn) {
      let token = localStorage.getItem('auth_token');
      userName = this.decodeService.getDecodedAccessToken(token).sub;
    }
    return userName;
  }

  logout() {
    localStorage.removeItem('auth_token');
    this.loggedIn = false;
    this.authNavStatusSource.next(false);
  }

  isLoggedIn() {
    return this.loggedIn;
  }

  //facebookLogin(accessToken: string) {
  //  let headers = new Headers();
  //  headers.append('Content-Type', 'application/json');
  //  let body = JSON.stringify({ accessToken });
  //  return this.http
  //    .post(
  //      this.baseUrl + '/externalauth/facebook', body, { headers })
  //    .map(res => res.json())
  //    .map(res => {
  //      localStorage.setItem('auth_token', res.auth_token);
  //      this.loggedIn = true;
  //      this.authNavStatusSource.next(true);
  //      return true;
  //    })
  //    .catch(this.handleError);
  //}
}
