
import * as jwt_decode from 'jwt-decode';
import { Injectable } from '@angular/core';

@Injectable()
export class DecodeService  {

  constructor() { };

  public getDecodedAccessToken(token: string): any {
    try {
      return jwt_decode(token);
    }
    catch (Error) {
      return null;
    }
  }
}
