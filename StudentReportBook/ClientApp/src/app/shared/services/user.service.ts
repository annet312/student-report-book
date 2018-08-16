import { Injectable } from '@angular/core';
import { Inject } from '@angular/core';

import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { HttpClient } from '@angular/common/http';

import { UserRegistration } from '../models/user.registration.interface';
import { ConfigService } from '../utils/config.service';
import { UserResponse } from '../models/UserResponse';
import { BaseService } from "./base.service";
import { AuthService } from "../../auth/auth.service";

import { Observable, of as observableOf } from 'rxjs';
import { BehaviorSubject } from 'rxjs/Rx';

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

  constructor(private http: Http, private authService: AuthService, private httpClient: HttpClient, private configService: ConfigService, @Inject('BASE_URL') baseUrl: string) {
    super();
    this.loggedIn = authService.isAuthenticated();
    this.authNavStatusSource.next(this.loggedIn);
    this.baseUrls = baseUrl;
  }

  register(email: string, password: string, firstName: string, lastName: string, role: string, department: string): Observable<boolean | {}> {
    console.log(email + '' + password);
    let body = JSON.stringify({ email, password, firstName, lastName, role, department});
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });
    return this.http.post(this.baseUrls + "api/accounts", body, options)
      .map(res => true)
      .catch(this.handleError);
  }

  login(userName: string, password: string) {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    return this.http
      .post(
        this.baseUrls + 'api/auth/login',
        JSON.stringify({ userName, password }), { headers }
      )
      .map(res => res.json())
      .map(res => {
        this.loggedIn = this.authService.setToken(res.auth_token);
        this.authNavStatusSource.next(true);
        return true;
      })
      .catch(this.handleError);
  }

  logout() {
    localStorage.removeItem('auth_token');
    localStorage.removeItem('current_role');
    this.loggedIn = false;
    this.authNavStatusSource.next(false);
  }

}
