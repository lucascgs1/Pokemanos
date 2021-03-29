import { Injectable } from '@angular/core';
import { CacheService } from './cache.service';

@Injectable({
  providedIn: 'root'
})
export class TokenService {
  public userToken: string = "";

  constructor(
    private cacheService: CacheService
  ) {
  }

  setToken(token: string) {
    this.cacheService.save({ data: token, key: "userToken", expirationMins: 120 });
    this.userToken = token;
  }

  getToken() {
    this.userToken = this.cacheService.load("userToken");

    return this.userToken;
  }

  isActive() {
    this.getToken();
    if (this.userToken === null || this.userToken === undefined) {
      return false;
    } else {
      return true;
    }
  }
}



